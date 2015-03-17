using UnityEngine;
using System.Collections;

public class AnimationHandler : MonoBehaviour
{
    //Fields
    //Public
    public Sprite[] walkFrames;
    public Sprite standing;
    public float frameTime;

    //Private
    private float currentTime;
    private int currentFrame;

    // Update is called once per frame
    void Update()
    {
        //If its the player
        if (gameObject.tag == "Player")
        {
            //If they are walking
            if (gameObject.GetComponent<PlayerController>().walking)
            {
                //If the time for the current frame is positive
                if (currentTime <= 0)
                {
                    currentFrame = (currentFrame + 1) % walkFrames.Length;
                    gameObject.GetComponent<SpriteRenderer>().sprite = walkFrames[currentFrame];
                    currentTime = frameTime;
                }
                else //If it is negative
                {
                    currentTime -= Time.deltaTime;
                }
            }
            else //If they arent walking
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = standing;
            }
        }
        //If it is an enemy
        else if (gameObject.tag == "Enemy")
        {
            //If theyre moving
            if (gameObject.GetComponent<EnemyScript>().direction.magnitude > 0)
            {
                //If the current frame time is positive
                if (currentTime <= 0)
                {
                    currentFrame = (currentFrame + 1) % walkFrames.Length;
                    gameObject.GetComponent<SpriteRenderer>().sprite = walkFrames[currentFrame];
                    currentTime = frameTime;
                }
                //If its negative
                else
                {
                    currentTime -= Time.deltaTime;
                }
            }
            //If they're not moving
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
    }
}
