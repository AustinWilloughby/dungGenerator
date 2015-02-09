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
    private GameObject pauseMenu;


    // Use this for initialization
    void Start()
    {
        arrowTimer = 0f;
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        pauseMenu = GameObject.Find("PauseMenu");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMover();
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
        arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, 97);
        arrowTimer = 1f;
    }

    void HandleInput() //Handles all player input
    {
        if (Time.timeScale > 0)
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.GetComponent<PauseScript>().PauseGame();
        }
    }

    private void PlayerMover()
    {
        if (Time.timeScale > 0)
        {
            //Calculates movement in x and y directions based on input
            float xMovement = Input.GetAxis("Horizontal");
            float yMovement = Input.GetAxis("Vertical");

            //Edit the players transform
            Vector2 translate = new Vector2(xMovement, yMovement);
            translate *= speed;
            transform.position = transform.position + (Vector3)translate;

            SpriteRotator(translate);
        }
    }
}
