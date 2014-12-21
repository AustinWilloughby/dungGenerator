using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    //Fields
    private float speed = .05f;
    private float attackRange = 2f;
    private int attackDamage = 2;
	
	// Update is called once per frame
	void Update () 
    {
        //Calculates movement in x and y directions based on input
        float xMovement = Input.GetAxis("Horizontal") * speed;
        float yMovement = Input.GetAxis("Vertical") * speed;

        //Edit the players transform
        Vector2 translate = transform.position;
        translate.x += xMovement;
        translate.y += yMovement;
        transform.position = translate;

        if (Input.GetKeyDown("space"))
        {
            Attack();
        }
	}

    void Attack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, transform.position) < attackRange)
            {
                enemy.GetComponent<StatTracker>().health -= attackDamage;
            }
        }
    }
}
