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
        if (gameObject.GetComponent<PlayerController>().walking)
        {
            if (currentTime <= 0)
            {
                currentFrame = (currentFrame + 1) % walkFrames.Length;
                gameObject.GetComponent<SpriteRenderer>().sprite = walkFrames[currentFrame];
                currentTime = frameTime;
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = standing;
        }
    }
}
