﻿using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    //Fields
    //Private
    private GameObject player;
    private Vector2 direction;
    private float speed = .5f;
    private float timer = 10f;
    private int damage = 2;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = mousePos - player.transform.position;
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
        if (Time.timeScale > 0)
        {
            timer -= Time.deltaTime;
            if (timer > 0) //If there is time left
            {
                //Move, and slow down speed
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
            else //If out of time, destroy arrow
            {
                GameObject.Destroy(gameObject);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other) //Triggers when a 2D collider intersects arrow collider
    {
        //Damage if its an enemy
        if (other.gameObject.tag == "Enemy")
        {
            if (speed != 0) //Non moving arrows aren't harmful
            {
                other.gameObject.GetComponent<StatTracker>().TakeDamage(damage);
                speed = 0;
                gameObject.transform.parent = other.gameObject.transform;
            }
        }
        else if (other.gameObject.tag == "Unshootable")
        {
            GameObject.Destroy(gameObject);
        }
        //Ignore if it is on layer9 "Entity"
        if (other.gameObject.layer == 11)
        {
            speed = 0;
        }
    }
}