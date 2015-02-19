using UnityEngine;
using System.Collections;

public class CollectableScript : MonoBehaviour
{
    //Fields
    //Public
    public int value = 1; //Value of the collectable

    //Private
    private GameObject player;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    //Events
    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects coin collider
    {
        if (other.gameObject == player) //If the gameobject is the player, then collect and destroy
        {
            //player.GetComponent<InventoryScript>().purseValue += value;
            GameObject.Destroy(gameObject);
        }

    }
}
