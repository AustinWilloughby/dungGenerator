using UnityEngine;
using System.Collections;

public class StatTracker : MonoBehaviour 
{
    //Fields
    //Public
    public int health = 10;
    public int energy = 10;
    public int purseValue = 0;

    //Methods
    public void TakeDamage(int damage) //Appropriately removes player health
    {
        health -= damage;
    }
}
