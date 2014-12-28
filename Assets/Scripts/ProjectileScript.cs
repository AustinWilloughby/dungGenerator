using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    //Fields
    //Private
    private GameObject shooter;
    private float angleFromUp;
    private float speed = .5f;
    private float timer = 2f;
    private Vector2 direction;
    private int damage = 2;

    // Use this for initialization
    void Start()
    {
        shooter = null;
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
                speed -= (.5f * Time.deltaTime);
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


    //Methods
    public void Setup(GameObject gunSlinger, float angle, Vector2 direc)
    {
        shooter = gunSlinger;
        transform.rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        direction = Vector2.ClampMagnitude(direc, 1f);
    }


    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects weapon collider
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<StatTracker>().TakeDamage(damage);
        }
    }
}