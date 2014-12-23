using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{
    //Fields
    private GameObject player;
    private float attackTimer = .01f;
    private bool attacking;
    public int damage = 2;

    // Use this for initialization
    void Start()
    {
        gameObject.renderer.enabled = false;
        attacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            transform.RotateAround(player.transform.position, Vector3.forward, -(360 * Time.deltaTime));
            if (attackTimer < 0)
            {
                gameObject.renderer.enabled = false;
                attacking = false;
                gameObject.SetActive(false);
            }
        }

    }

    public void Attack()
    {
        attacking = true;
        gameObject.renderer.enabled = true;
        attackTimer = .334f;
    }

    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects weapon collider
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<StatTracker>().TakeDamage(damage);
        }
    }
}
