using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour
{
    //Fields
    public float damping = 0f; //Delay between target and camera movement
    private Vector3 velocity = Vector3.zero;
    private GameObject followTarget;

    // Use this for initialization
    void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget)
        {
            //Calculates the necessary camera movements to smoothly follow the player
            Vector3 current = camera.WorldToViewportPoint(followTarget.transform.position);
            Vector3 change = followTarget.transform.position - camera.ViewportToWorldPoint(new Vector3(.5f, .5f, current.z));
            Vector3 target = transform.position + change;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, damping);
        }
    }

    public void ChangeCamTarget(GameObject target) //Changes what the camera follows
    {
        followTarget = target;
    }
}