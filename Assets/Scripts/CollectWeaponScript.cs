using UnityEngine;
using System.Collections;

public class CollectWeaponScript : MonoBehaviour
{
    //Fields
    //Public
    public int attackDamage = 3; //Attack damage of the collectable weapon

    //Private
    private GameObject player;
    private GameObject playersWeapon;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playersWeapon = GameObject.Find("PlayerSword");
    }


    //Events
    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects weapon collider
    {
        if (other.gameObject == player) //If the gameobject is the player, then collect and destroy
        {
            //If the players current weapon is weaker than the collectable one
            if (playersWeapon.GetComponent<SwordScript>().damage < attackDamage)
            {
                playersWeapon.GetComponent<SwordScript>().damage = attackDamage;
                GameObject.Destroy(gameObject);
            }
        }

    }
}
