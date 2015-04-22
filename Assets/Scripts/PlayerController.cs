using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //Fields
    public GameObject arrowPrefab;
    public bool walking;
    public PlayerClass role;
    public bool alive = true;
    public float speed = .05f;

    //Private
    private GameObject weapon;
    private float arrowTimer;
    private GameObject pauseMenu;
    private bool mouse1Down = false;
    private bool mouse2Down = false;
    private FxHandler soundFX;
    private GameObject deathMenu;


    // Use this for initialization
    void Start()
    {
        walking = false;
        arrowTimer = 0f;
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        pauseMenu = GameObject.Find("PauseMenu");
        soundFX = GameObject.Find("Main Camera").GetComponent<FxHandler>();
        deathMenu = GameObject.Find("DeathMenu");
        deathMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            PlayerMover();
            HandleInput();
            DeathCheck();
            arrowTimer -= Time.deltaTime;
        }
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
            AudioSource[] allAudio = GameObject.FindObjectsOfType<AudioSource>();
            for (int i = 0; i < allAudio.Length; i++)
            {
                if (allAudio[i].isPlaying)
                {
                    allAudio[i].Stop();
                }
            }
            soundFX.deathSound.Play();
            Time.timeScale = 0;
            alive = false;
            deathMenu.SetActive(true);
            GameObject.Find("Value Text").GetComponent<TextMesh>().text = "Loot Value: " + gameObject.GetComponent<InventoryManager>().CoinCount;
        }
    }

    void FireArrow() //Fires an arrow
    {
        GameObject arrow = (GameObject)Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, 97);
        if (role != PlayerClass.Assassin)
        {
            arrowTimer = 1f;
        }
        else
        {
            arrowTimer = .7f;
        }
        if (arrowPrefab.name == "Arrow")
        {
            soundFX.arrowSound.Play();
        }
        else
        {
            soundFX.starSound.Play();
        }
    }

    void HandleInput() //Handles all player input
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetAxis("Swing") > 0)
            {
                if (mouse1Down == false)
                {
                    if (role != PlayerClass.Assassin)
                    {
                        mouse1Down = true;
                        weapon.SetActive(true);
                        weapon.GetComponent<SwordScript>().Attack();
                        soundFX.swordSound.Play();
                    }
                    else
                    {
                        if (arrowTimer < 0)
                        {
                            mouse2Down = true;
                            FireArrow();
                        }
                    }
                }
            }
            else
            {
                mouse1Down = false;
            }

            if (role == PlayerClass.Knight)
            {
                if (Input.GetAxis("Shoot") > 0)
                {
                    if (mouse2Down == false)
                    {
                        if (arrowTimer < 0)
                        {
                            mouse2Down = true;
                            FireArrow();
                        }
                    }
                }
                else
                {
                    mouse2Down = false;
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
            if (translate.magnitude > 0)
            {
                walking = true;
            }
            else
            {
                walking = false;
            }
            SpriteRotator(translate);
        }
    }
}

//The three playable classes for the player
public enum PlayerClass
{
    Knight,
    Adventurer,
    Assassin
};
