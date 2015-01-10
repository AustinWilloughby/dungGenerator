using UnityEngine;
using System.Collections;

public class RopeScript : MonoBehaviour
{
    //Fields
    //Public
    public GameObject ropelessHole;
    public GameObject ropeHolePrefab;

    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects rope
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject ropeHole = (GameObject)Instantiate(ropeHolePrefab);
            ropeHole.transform.position = ropelessHole.transform.position;
            GameObject.Destroy(ropelessHole);
            GameObject.Destroy(gameObject);
        }
    }
}
