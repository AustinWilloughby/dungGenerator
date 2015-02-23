using UnityEngine;
using System.Collections;

public class RopeScript : MonoBehaviour
{
    //Fields
    //Private
    private InventoryManager inventory;


    //Events
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
    }

    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects rope
    {
        if (other.gameObject.tag == "Player")
        {
            inventory.ropeCollected = true;
            GameObject.Destroy(gameObject);
        }
    }
}
