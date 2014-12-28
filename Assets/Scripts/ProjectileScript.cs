using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    //Fields
    //Private
    private GameObject player;
    private GameObject playerForward;
    private Vector2 direction;
    private float angleFromUp;
    private float speed = .5f;
    private float timer = 1.5f;
    private int damage = 5;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerForward = GameObject.Find("PlayerForward");

        direction = playerForward.transform.position - player.transform.position;
        direction = Vector2.ClampMagnitude(direction, 1f);

        float angleFromUp = Vector2.Angle(Vector2.up, direction);
        if (direction.x < 0)
        {
            angleFromUp *= -1;
        }
        transform.rotation = Quaternion.AngleAxis(angleFromUp, -Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            transform.position += (Vector3)Vector2.ClampMagnitude(direction, speed);
            if (speed > 0)
            {
                speed -= (.7f * Time.deltaTime);
            }
            else
            {
                speed = 0;
            }
        }
        else
        {
            speed = .5f;
            GameObject.Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects weapon collider
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<StatTracker>().TakeDamage(damage);
        }
        if (other.gameObject.tag != "Player")
        {
            GameObject.Destroy(gameObject);
        }
    }
}