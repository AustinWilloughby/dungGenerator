using UnityEngine;
using System.Collections;

public class CharacterMover : MonoBehaviour 
{
    //Fields
    private float speed = .05f;
	
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
	}
}
