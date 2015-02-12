using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour
{
    //Fields
    //Public
    public int damage = 2;

    //Private
    private GameObject player;
    private float attackTimer = .01f;
    private bool attacking;
    private GameObject sheath;

    // Use this for initialization
    void Start()
    {
        gameObject.renderer.enabled = false;
        attacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        sheath = GameObject.Find("Sheath");
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            //Rotates sword in an arc in front of player
            transform.RotateAround(player.transform.position, Vector3.forward, (720 * Time.deltaTime));
            if (attackTimer < 0)
            {
                attacking = false;
                gameObject.renderer.enabled = false;
                transform.position = sheath.transform.position;
                transform.rotation = sheath.transform.rotation;
                gameObject.SetActive(false);
            }
        }

    }
    //Events
    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects weapon collider
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<StatTracker>().TakeDamage(damage);
        }
    }


    //Methods
    public void Attack() //Handles using weapon
    {
        if (attacking == false)
        {
            attacking = true;
            gameObject.renderer.enabled = true;
            attackTimer = .167f;
        }
    }
}
