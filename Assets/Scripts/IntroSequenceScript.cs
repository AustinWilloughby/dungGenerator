using UnityEngine;
using System.Collections;

public class IntroSequenceScript : MonoBehaviour
{
    //Fields
    //Public
    public float timeTillMenu;
    public float transitionTime;
    public AudioSource mainMusic;
    public Sprite[] frames;
    public Sprite darkFrame;


    //Private
    private float timePerScreen;
    private float nextEventTimer;
    private int currentEvent = 0;

    // Use this for initialization
    void Start()
    {
        timePerScreen = (timeTillMenu - (5*transitionTime))/4;
        nextEventTimer = transitionTime;
        mainMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
