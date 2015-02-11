using UnityEngine;
using System.Collections;

public class BossSwordScript : MonoBehaviour 
{
    //Fields
    //Public
    public int damage = 5;

    //Events
    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects weapon collider
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<StatTracker>().TakeDamage(damage);
        }
    }
}
