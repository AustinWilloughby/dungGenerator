using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            GameObject boss = GameObject.Find("Boss");
            GameObject playerSpawn = GameObject.Find("PlayerSpawn");
            playerSpawn.transform.position = (Vector2)transform.position;
            transform.position = hidingSpot;
            //Collects all level specific things and removes them
            List<GameObject> deleteThese = new List<GameObject>();
            deleteThese.AddRange(GameObject.FindGameObjectsWithTag("Collectable"));
            deleteThese.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            deleteThese.AddRange(GameObject.FindGameObjectsWithTag("DeletePerLevel"));
            deleteThese.Remove(boss);
            GameObject[] delete = deleteThese.ToArray();
            for(int i = 0; i < delete.Length; i++)
            {
                GameObject.Destroy(delete[i]);
                delete[i] = null;
            }
            dungeonGenerator.NewLevel();
        }
    }
}
