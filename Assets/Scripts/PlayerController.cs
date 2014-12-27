using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //Fields
    //Private
    private float speed = .05f;
    private GameObject weapon;


    // Use this for initialization
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        //Calculates movement in x and y directions based on input
        float xMovement = Input.GetAxis("Horizontal") * speed;
        float yMovement = Input.GetAxis("Vertical") * speed;

        //Edit the players transform
        Vector2 translate = new Vector2(xMovement, yMovement);
        transform.position = transform.position + (Vector3)translate;

        SpriteRotator(translate);
        HandleInput();
        DeathCheck();
    }


    //Methods
    void SpriteRotator(Vector2 moveDirection) //Rotates sprite based off of movement direction
    {
        //If moving
        if (moveDirection.magnitude != 0)
        {
            float angleFromUp = Vector2.Angle(Vector2.up, moveDirection);
            if (moveDirection.x < 0) //Allows for 360 rotation
            {
                angleFromUp *= -1;
            }
            transform.rotation = Quaternion.AngleAxis(angleFromUp, -Vector3.forward);
        }
    }

    void DeathCheck() //Checks if player dies, and handles accordingly
    {
        if (gameObject.GetComponent<StatTracker>().health <= 0)
        {
            gameObject.GetComponent<StatTracker>().TakeDamage(-10);
            GameObject playerSpawn = GameObject.Find("PlayerSpawn");
            gameObject.GetComponent<StatTracker>().ApplyHealing(100000);
            transform.position = playerSpawn.transform.position;
        }
    }

    void HandleInput() //Handles all player input
    {
        if (Input.GetKeyDown("space"))
        {
            weapon.SetActive(true);
            weapon.GetComponent<SwordScript>().Attack();
        }
    }
}
