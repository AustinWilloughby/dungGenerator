using UnityEngine;
using System.Collections;

public class SpearScript : MonoBehaviour
{
    //Fields
    private GameObject enemy;
    private float attackLength = .01f;
    private bool attacking;
    public int damage = 2;

    // Use this for initialization
    void Start()
    {
        gameObject.renderer.enabled = false;
        attacking = false;
        enemy = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackLength > 0)
        {
            attackLength -= Time.deltaTime;

            if (attackLength < 0)
            {
                attacking = false;
                gameObject.renderer.enabled = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void Attack() //Handles using weapon
    {
        if (attacking == false)
        {
            attacking = true;
            gameObject.renderer.enabled = true;
            attackLength = .2f;
        }
    }

    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects weapon collider
    {
        //If the target hit is the player, damage the player
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<StatTracker>().TakeDamage(damage);
        }
    }
}
