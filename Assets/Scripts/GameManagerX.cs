using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public GameObject gameOverScreen;
    public GameObject gameMenuScreen;
    public GameObject mainMenuScreen;
    public GameObject player;
    public Player playerX;

    public bool isGameActive;
    public int difficulty;
    private int score;


    //Spawn Variables
    public GameObject[] enemyPrefab;

    private float spawnRangeX = 35;
    private float spawnZMin = -35; // set min spawn Z
    private float spawnZMax = 35; // set max spawn Z

    public int enemyCount;
    public int waveCount = 1;




    void Start()
    {
        playerX = FindObjectOfType<Player>();
    }

    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(isGameActive == true)
        {
            //playerX.playerRb.freezeRotation = false;

            //Spawns Enemy
            if (enemyCount == 0)
            {
                SpawnEnemyWave(waveCount);
            }

            //Player Dies
            if (playerX.curHealth <= 0)
            {
                GameOver();
            }

            gameMenuScreen.SetActive(true);
        }

        //if(isGameActive == false)
        //{
        //    playerX.playerRb.constraints = RigidbodyConstraints.FreezeAll;
        //}

    }





    //Game Starts
    public void StartGame(int difficulty)
    {
        mainMenuScreen.SetActive(false);
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        Cursor.lockState = CursorLockMode.Locked;
    }

    //GameOver
    private void GameOver()
    {
            gameOverScreen.SetActive(true);
            isGameActive = false;
            Debug.Log("Player Has Died");
            Cursor.lockState = CursorLockMode.None;
            
            //Destroy Enemies
            GameObject[] destroyEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject target in destroyEnemies)
            {
                GameObject.Destroy (target);
            }
            
    }





    // Spawn number of enemies based on wave number
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[enemyIndex], GenerateSpawnPosition(), enemyPrefab[enemyIndex].transform.rotation);
        }

        waveCount++;

        waveText.text = "Wave: " + (waveCount-1);
    }

    // Generate random spawn position for enemy
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 2, zPos);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Kills: " + score;
    }


    public void restart()
    {
        gameOverScreen.SetActive(false);
        playerX.curHealth = 100;
        playerX.healthbar.fillAmount = 100;
        waveCount = 1;
        StartGame(1);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("My Game");  
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }

}
