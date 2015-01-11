using UnityEngine;
using System.Collections;

public class RopeScript : MonoBehaviour
{
    //Fields
    //Private
    private GameObject ropelessHole;
    private GameObject ropeHole;
    private Vector3 holdingCell = new Vector3(-20, -20, 30);


    //Events
    void Start()
    {
        ropelessHole = GameObject.Find("EmptyDungeonHole");
        ropeHole = GameObject.Find("DungeonHole");
    }

    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects rope
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 holePos = ropelessHole.transform.position;
            ropelessHole.transform.position = holdingCell;
            ropeHole.transform.position = holePos;

            GameObject.Destroy(gameObject);
        }
    }
}
