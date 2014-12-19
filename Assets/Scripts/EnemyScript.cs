using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public float viewDistance = 10f;
    public float speed = .05f;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < viewDistance)
        {
            transform.position += Arrive(player, 3);
        }
    }

    Vector3 Seek(GameObject target) //Basic Seeking Behavior
    {
        Vector3 seek = Vector3.zero;
        seek = target.transform.position - transform.position;
        seek = Vector3.Normalize(seek);
        seek = seek * speed;
        return seek;
    }

    Vector3 Arrive(GameObject target, float slowRadius) //Advanced seeking with slow down at target
    {
        Vector3 arrive = Seek(target);
        if (Vector3.Distance(transform.position, target.transform.position) < slowRadius)
        {
            arrive = Vector3.Normalize(arrive);
            arrive = arrive * speed;
            arrive = arrive * ((Vector3.Distance(transform.position, player.transform.position) - 1.5f) / slowRadius);
        }
        return arrive;
    }
}
