using UnityEngine;
using System.Collections;

public class StatTracker : MonoBehaviour
{
    //Fields
    //Public
    public int health = 10;
    public int energy = 10;

    //Private
    private int maxHealth;


    //Attributes
    public int MaxHealth
    {
        get { return maxHealth; }
    }


    // Use this for initialization
    void Start()
    {
        maxHealth = health;
    }


    //Methods
    public void TakeDamage(int damage) //Appropriately removes player health
    {
        if (damage < 0)
        {
            damage *= -1;
        }
        health -= damage;
    }

    public void ApplyHealing(int healing) //Appropriately heals the player
    {
        if (healing < 0)
        {
            healing *= -1;
        }

        health += healing;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
