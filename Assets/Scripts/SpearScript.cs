using UnityEngine;
using System.Collections;

public class SpearScript : MonoBehaviour
{
    //Fields
    //Public
    public int damage = 2;

    //Private
    private float attackLength = .01f;
    private bool attacking;


    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        attackLength -= Time.deltaTime;

        //If they are done attacking, stop
        if (attackLength <= 0)
        {
            attacking = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.SetActive(false);
        }

    }


    //Events
    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects weapon collider
    {
        //If the target hit is the player, damage the player
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<StatTracker>().TakeDamage(damage);
        }
    }


    //Methods
    public void Attack() //Handles using weapon
    {
        //If not attacking, attack
        if (attacking == false)
        {
            attacking = true;
            gameObject.GetComponent<Renderer>().enabled = true;
            attackLength = .2f;
        }
    }
}
