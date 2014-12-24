using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour 
{
    //Fields
    //Public 
    public float speed = .05f; //Movement speed

    //Private
    private float wanderTimer = 0f; // Time that keeps track of current wander time

    //Protected
    protected Vector2 wanderTarget = Vector2.zero; //Target of wandering


    //Methods
    protected Vector2 Seek(Vector2 target) //Basic Seeking Behavior
    {
        Vector2 seek = Vector2.zero;
        seek = target - (Vector2)transform.position;
        seek = Vector2.ClampMagnitude(seek, 1);
        seek = seek * speed;
        return seek;
    }

    protected Vector2 Arrive(Vector2 target, float slowRadius) //Advanced seeking with slow down at target
    {
        Vector2 arrive = Seek(target);
        if (Vector2.Distance(transform.position, target) < slowRadius)
        {
            arrive = Vector2.ClampMagnitude(arrive, 1);
            arrive = arrive * speed;
            arrive = arrive * ((Vector2.Distance(transform.position, target) - 1.5f) / slowRadius);
        }
        return arrive;
    }

    protected Vector2 Wander() //Advanced arrival that targets a random point within a specified range from the vehicle
    {
        if (wanderTimer <= 0)
        {
            do
            {
                wanderTarget.x = Random.Range(transform.position.x - 15, transform.position.x + 15);
                wanderTarget.y = Random.Range(transform.position.y - 15, transform.position.y + 15);
            } while (Vector2.Distance(wanderTarget, transform.position) < 10);

            wanderTimer = Random.Range(1f, 2f);
        }
        wanderTimer -= Time.deltaTime;
        return Arrive(wanderTarget, 1)/5;
    }

    protected void SpriteRotator(Vector2 moveDirection) //Rotates sprite based off of movement direction
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
