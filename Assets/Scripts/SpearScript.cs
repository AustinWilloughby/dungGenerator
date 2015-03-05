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
    private GameObject sheath;
    private GameObject point;


    // Use this for initialization
    void Start()
    {
        attacking = false;
        sheath = gameObject.transform.parent.FindChild("SpearSheath").gameObject;
        point = gameObject.transform.parent.FindChild("SpearPoint").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //If they are done attacking, stop
        if (attackLength <= 0)
        {
            transform.position = sheath.transform.position;
            attacking = false;
        }
        else
        {
            attackLength -= Time.deltaTime;
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
            transform.position = point.transform.position;
            attackLength = .2f;
        }
    }
}
