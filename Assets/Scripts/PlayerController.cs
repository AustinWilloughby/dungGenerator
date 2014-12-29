using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //Fields
    public GameObject arrowPrefab;

    //Private
    private float speed = .05f;
    private GameObject weapon;
    private float arrowTimer;
    private float lastAngle;
    private Vector2 lastDirection;


    // Use this for initialization
    void Start()
    {
        lastDirection = Vector2.up;
        lastAngle = 0f;
        arrowTimer = 0f;
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
        arrowTimer -= Time.deltaTime;
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
            lastAngle = angleFromUp;
            lastDirection = moveDirection;
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

    void FireArrow()
    {
        GameObject arrow = (GameObject)Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrowTimer = 2f;
    }

    void HandleInput() //Handles all player input
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.SetActive(true);
            weapon.GetComponent<SwordScript>().Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (arrowTimer < 0)
            {
                FireArrow();
            }
        }
    }
}
