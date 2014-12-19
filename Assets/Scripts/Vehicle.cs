using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour 
{
    public float speed = .05f;
    private float wanderTimer = 0f;
    private Vector2 wanderTarget = Vector2.zero;

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
            arrive = arrive * ((Vector2.Distance(transform.position, target) - 1f) / slowRadius);
        }
        return arrive;
    }

    protected Vector2 Wander()
    {
        if (wanderTimer >= 0)
        {
            do
            {
                wanderTarget.x = Random.Range(transform.position.x - 15, transform.position.x + 15);
                wanderTarget.y = Random.Range(transform.position.y - 15, transform.position.y + 15);
            } while (Vector2.Distance(wanderTarget, transform.position) < 10);

            print(wanderTarget);
            wanderTimer = Random.Range(10f, 20f);
        }
        wanderTimer -= Time.deltaTime;
        return Arrive(wanderTarget, 1);
    }
}
