using UnityEngine;
using System.Collections;

public class CharacterMover : MonoBehaviour {
    //Fields
    private float speed = .05f;
	
	// Update is called once per frame
	void Update () 
    {
        float xMovement = Input.GetAxis("Horizontal") * speed;
        float yMovement = Input.GetAxis("Vertical") * speed;

        Vector3 translate = transform.position;
        translate.x += xMovement;
        translate.y += yMovement;

        transform.position = translate;

	}
}
