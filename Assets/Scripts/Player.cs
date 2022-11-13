using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManagerX gameManagerX;

    private Rigidbody playerRb;
    public float horizontalInput;
    public float verticalInput;
    public float speed = 2.0f;

    public Image healthbar;
    public float curHealth;
    public float damage;

    void Start()
    {
        gameManagerX = FindObjectOfType<GameManagerX>();
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Movement
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
        
        //Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        //movementDirection.Normalize();

        //transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        //if (movementDirection.magnitude >= 0.1f)
        //{
        //    controller.Move(movementDirection * speed * Time.deltaTime);
        //}

        //if (movementDirection != Vector3.zero)
        //{
        //    transform.forward = movementDirection;
        //}


        

        if(gameManagerX.isGameActive == false)
        {
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Player is Frozen");
        }

    }

    //Player takes Damage
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            curHealth = curHealth - damage;
            healthbar.fillAmount = curHealth / 100;
            Debug.Log("Player Has taken damage");
        }
    }

}
