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
    private FxHandler soundFX;


    //Attributes
    public int MaxHealth
    {
        get { return maxHealth; }
    }


    // Use this for initialization
    void Start()
    {
        maxHealth = health;
        soundFX = GameObject.Find("Main Camera").GetComponent<FxHandler>();
    }

    void Update()
    {
        if (redTimer > 0) //Sustains shade change
        {
            redTimer -= Time.deltaTime;
        }
        else //Resets default shade
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }


    //Methods
    public void TakeDamage(int damage) //Appropriately removes player health
    {
        if (damage < 0)
        {
            damage *= -1;
        }
        if(gameObject.name == "Enemy(Clone)")
        {
            soundFX.enemySound.Play();
        }
        if (gameObject.name == "Boss")
        {
            soundFX.bossSound.Play();
        }
        redTimer = .5f;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        health -= damage;
    }

    public void ApplyHealing(int healing) //Appropriately heals the player
    {
        if (healing < 0)
        {
            healing *= -1;
        }
        redTimer = .5f;
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        health += healing;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
