using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
    public float viewDistance;
    public float speed;
    private GameObject player;

	// Use this for initialization
	void Start () 
    {
        viewDistance = 10f;
        speed = .005f;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Vector3.Distance(transform.position, player.transform.position) < viewDistance)
        {
            transform.position += Seek(player);
        }
	}

    Vector3 Seek(GameObject target)
    {
        Vector3 seek = Vector3.zero;
        seek = target.transform.position - transform.position;
        Vector3.ClampMagnitude(seek, speed*Time.deltaTime);
        return seek;
    }
}
