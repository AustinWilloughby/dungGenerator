using UnityEngine;
using System.Collections;

public class IntroSequenceScript : MonoBehaviour
{
    //Fields
    //Public
    public float timeTillMenu;
    public float transitionTime;
    public AudioSource mainMusic;

    //Private
    private float timePerScreen;
    private float nextEventTimer;
    private int currentEvent = 0;
    private GameObject textWheel;

    // Use this for initialization
    void Start()
    {
        timePerScreen = (timeTillMenu - (5*transitionTime))/4;
        nextEventTimer = transitionTime;
        textWheel = GameObject.Find("TextWheel");
        mainMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentEvent)
        {
            case 0:
                if (nextEventTimer > 0)
                {
                    nextEventTimer -= Time.deltaTime;
                    Vector3 rot = textWheel.transform.rotation.eulerAngles;
                    rot.x += Time.deltaTime * (1 / transitionTime) * 90;
                    textWheel.transform.rotation = Quaternion.Euler(rot);
                }
                else
                {
                    nextEventTimer = timePerScreen;
                    currentEvent++;
                }
                break;

            case 2:
            case 4:
            case 6:
                if (nextEventTimer > 0)
                {
                    nextEventTimer -= Time.deltaTime;
                    Vector3 rot = textWheel.transform.rotation.eulerAngles;
                    rot.z -= Time.deltaTime * (1 / transitionTime) * 90;
                    textWheel.transform.rotation = Quaternion.Euler(rot);
                }
                else
                {
                    nextEventTimer = timePerScreen;
                    currentEvent++;
                }
                break;

            case 8:
                if (nextEventTimer > 0)
                {
                    nextEventTimer -= Time.deltaTime;
                    Vector3 rot = gameObject.transform.rotation.eulerAngles;
                    rot.x += Time.deltaTime * (1 / transitionTime) * 90;
                    gameObject.transform.rotation = Quaternion.Euler(rot);
                }
                else
                {
                    nextEventTimer = timePerScreen;
                    currentEvent++;
                }
                break;

            default:
                if (nextEventTimer > 0)
                {
                    nextEventTimer -= Time.deltaTime;
                }
                else
                {
                    nextEventTimer = transitionTime;
                    currentEvent++;
                }
                break;

        }
    }
}
