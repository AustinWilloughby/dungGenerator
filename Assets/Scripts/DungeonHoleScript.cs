using UnityEngine;
using System.Collections;

public class DungeonHoleScript : MonoBehaviour
{
    //Fields
    //Public
    public DungeonGeneration dungeonGenerator;

    //Private
    private Vector3 hidingSpot = new Vector3(-20, -20, 30);

    //Events
    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects DungeonHole collider
    {
        if (other.gameObject.tag == "Player")
        {
            transform.position = hidingSpot;
            dungeonGenerator.NewLevel();
        }
    }
}
