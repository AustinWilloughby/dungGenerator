using UnityEngine;
using System.Collections;

public class KeepInsideScript : MonoBehaviour
{
    //Fields
    //Private
    private GameObject player;
    private Dungeon dungeon;
    private GameObject playerSpawn;

    //Events
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerSpawn = GameObject.Find("PlayerSpawn");
        dungeon = gameObject.GetComponent<Dungeon>();
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(dungeon.size.x, dungeon.size.y);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(dungeon.size.x / 2 - .5f, dungeon.size.y / 2 - .5f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //If the player leaves the overall dungeon collider he gets sent back to the spawn
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = (Vector2)playerSpawn.transform.position;
        }
    }
}
