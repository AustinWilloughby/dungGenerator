using UnityEngine;
using System.Collections;

public class StatTracker : MonoBehaviour 
{
    //Fields
    public int health = 10;
    public int energy = 10;

    //Attributes

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (health <= 0)
        {
            GameObject.Destroy(this);
        }
	}
}
