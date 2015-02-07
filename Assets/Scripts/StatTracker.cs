using UnityEngine;
using System.Collections;

public class StatTracker : MonoBehaviour
{
    //Fields
    //Public
    public int health = 10;

    //Private
    private int maxHealth;
    private float redTimer;


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

    void Update()
    {
        if (redTimer > 0)
        {
            redTimer -= Time.deltaTime;
        }
        else
        {
            gameObject.renderer.material.color = Color.white;
        }
    }


    //Methods
    public void TakeDamage(int damage) //Appropriately removes player health
    {
        if (damage < 0)
        {
            damage *= -1;
        }
        redTimer = .5f;
        gameObject.renderer.material.color = Color.red;
        health -= damage;
    }

    public void ApplyHealing(int healing) //Appropriately heals the player
    {
        if (healing < 0)
        {
            healing *= -1;
        }
        redTimer = .5f;
        gameObject.renderer.material.color = Color.yellow;
        health += healing;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
