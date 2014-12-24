using UnityEngine;
using System.Collections;

public class SpearScript : MonoBehaviour
{
    //Fields
    //Public
    public int damage = 2;

    //Private
    private GameObject enemy;
    private float attackLength = .01f;
    private bool attacking;


    // Use this for initialization
    void Start()
    {
        gameObject.renderer.enabled = false;
        attacking = false;
        enemy = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        attackLength -= Time.deltaTime;

        //If they are done attacking, stop
        if (attackLength <= 0)
        {
            attacking = false;
            gameObject.renderer.enabled = false;
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
            gameObject.renderer.enabled = true;
            attackLength = .2f;
        }
    }
}
