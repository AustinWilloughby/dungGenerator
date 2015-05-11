using UnityEngine;
using System.Collections;

public class RopeScript : MonoBehaviour
{
    //Fields
    //Private
    private InventoryManager inventory;
    private FxHandler soundFX;

    //Events
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        soundFX = GameObject.Find("Main Camera").GetComponent<FxHandler>();
    }

    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects rope
    {
        if (other.gameObject.tag == "Player")
        {
            try
            {
                soundFX.ropeSound.Play();
                inventory.ropeCollected = true;
                GameObject.Destroy(gameObject);
            }
            catch { }
        }
    }
}
