using UnityEngine;
using System.Collections;

public class ObstacleAvoidance : MonoBehaviour
{
    //Fields
    //Private
    private EnemyScript parent;

    // Use this for initialization
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<EnemyScript>();
    }

    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects obstacle avoider
    {
        if (parent.playerSeenLast)
        {
            if (other.gameObject.tag == "Wall")
            {
                Vector2 avoid = parent.gameObject.transform.position - other.gameObject.transform.position;
                avoid *= 2000;
                parent.direction += avoid;
            }
        }
    }
}
