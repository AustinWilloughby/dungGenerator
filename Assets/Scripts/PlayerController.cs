using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    //Fields
    private float speed = .05f;
    private float attackRange = 2f;
    private int attackDamage = 2;
    private bool goingRight = false;
    private bool goingUp = false;
	
	// Update is called once per frame
	void Update () 
    {
        //Calculates movement in x and y directions based on input
        float xMovement = Input.GetAxis("Horizontal") * speed;
        float yMovement = Input.GetAxis("Vertical") * speed;

        //Edit the players transform
        Vector2 translate = new Vector2(xMovement, yMovement);
        transform.position = transform.position + (Vector3)translate;

        SpriteRotator(translate);

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

    void SpriteRotator(Vector2 moveDirection) //Rotates sprite based off of movement direction
    {
        if (moveDirection.magnitude != 0)
        {
            float angleFromUp = Vector2.Angle(Vector2.up, moveDirection);
            if (moveDirection.x < 0)
            {
                angleFromUp *= -1;
            }
            transform.rotation = Quaternion.AngleAxis(angleFromUp, -Vector3.forward);
        }
    }
}
