using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour
{
    //Fields
    //Public
    public int damage = 2;
    public bool isWhip = false;

    //Private
    private GameObject player;
    private float attackTimer = .01f;
    private bool attacking;
    private GameObject sheath;
    private bool inWall;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        inWall = false;
        attacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        sheath = GameObject.Find("Sheath");
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 0)
        {
            if (inWall)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<Renderer>().enabled = true;
            }
            attackTimer -= Time.deltaTime;
            //Rotates sword in an arc in front of player
            if (!isWhip)
            {
                transform.RotateAround(player.transform.position, Vector3.forward, (720 * Time.deltaTime));
            }
            else
            {
                if (attackTimer > .0835f)
                {
                    transform.localScale = new Vector3(1.44f, transform.localScale.y, transform.localScale.z);
                    transform.RotateAround(player.transform.position, Vector3.forward, (720 * Time.deltaTime * 2));
                }
                else
                {
                    transform.localScale = new Vector3(-1.44f, transform.localScale.y, transform.localScale.z);
                    transform.RotateAround(player.transform.position, Vector3.forward, (-720 * Time.deltaTime * 2));
                }
            }
            if (attackTimer < 0)
            {
                attacking = false;
                gameObject.GetComponent<Renderer>().enabled = false;
                transform.position = sheath.transform.position;
                transform.rotation = sheath.transform.rotation;
                gameObject.SetActive(false);
            }
        }
    }
    //Events
    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects weapon collider
    {
        if (!inWall)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<StatTracker>().TakeDamage(damage);
            }
        }
        if (other.gameObject.tag == "Wall")
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            inWall = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            inWall = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            inWall = false;
        }
    }


    //Methods
    public void Attack() //Handles using weapon
    {
        if (attacking == false)
        {
            attacking = true;
            if (!inWall)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
            }
            attackTimer = .167f;
        }
    }
}