using UnityEngine;
using System.Collections;

public class CollectableScript : MonoBehaviour {

    private GameObject player;
    public int value = 1; //Value of the collectable

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects coin collider
    {
        if (other.gameObject == player) //If the gameobject is the player, then collect and destroy
        {
            player.GetComponent<StatTracker>().purseValue += value;
            GameObject.Destroy(gameObject);
        }

    }
}
