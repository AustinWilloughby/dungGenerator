using UnityEngine;
using System.Collections;

public class CollectableScript : MonoBehaviour
{
    //Fields
    //Public
    public int value = 1; //Value of the collectable

    //Private
    private GameObject player;
    private FxHandler soundFX;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        soundFX = GameObject.Find("Main Camera").GetComponent<FxHandler>();
    }


    //Events
    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects coin collider
    {
        if (other.gameObject == player) //If the gameobject is the player, then collect and destroy
        {
            player.GetComponent<InventoryManager>().AddCoinValue(value);
            soundFX.coinSound.Play();
            GameObject.Destroy(gameObject);
        }

    }
}
