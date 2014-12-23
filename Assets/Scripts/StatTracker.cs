using UnityEngine;
using System.Collections;

public class StatTracker : MonoBehaviour 
{
    //Fields
    public int health = 10;
    public int energy = 10;
    public int purseValue = 0;

    //Appropriately removes player health
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
