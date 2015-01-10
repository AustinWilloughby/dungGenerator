using UnityEngine;
using System.Collections;

public class DungeonHoleScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects DungeonHole collider
    {
        if (other.gameObject.tag == "Player")
        {
            //Generate new level
        }
    }
}
