using UnityEngine;
using System.Collections;

public class PotionScript : MonoBehaviour
{
    //Fields
    //Public
    public int healingDone = 5; //How much the potions heals

    //Private
    private GameObject player;
    private InventoryManager inventory;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
    }


    //Events
    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects potion collider
    {
        if (other.gameObject == player) //If the gameobject is the player, then collect and destroy
        {
            inventory.potionCount++;
            GameObject.Destroy(gameObject);
        }

    }
}
