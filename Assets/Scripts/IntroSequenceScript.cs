using UnityEngine;
using System.Collections;

public class IntroSequenceScript : MonoBehaviour
{
    //Fields
    //Public
    public float timeTillMenu;
    public float transitionTime;
    public AudioSource mainMusic;

    public TextMesh austinW;
    public TextMesh chrisC;
    public TextMesh marisaC;
    public GameObject[] menu;

    //Private
    private float timePerScreen;
    private float nextEventTimer;
    private int currentEvent = 0;

    // Use this for initialization
    void Start()
    {
        timePerScreen = (timeTillMenu - (7 * transitionTime)) / 4;
        nextEventTimer = transitionTime;
        mainMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEvent < 10)
        {
            switch (currentEvent)
            {
                case 0:
                    if (nextEventTimer > 0)
                    {
                        nextEventTimer -= Time.deltaTime;
                        Color fade = austinW.color;
                        fade.a = Mathf.Lerp(1, 0, nextEventTimer / transitionTime);
                        austinW.color = fade;
                    }
                    else
                    {
                        currentEvent++;
                        nextEventTimer = timePerScreen;
                    }
                    break;

                case 1:
                case 4:
                case 7:
                    if (nextEventTimer > 0)
                    {
                        nextEventTimer -= Time.deltaTime;
                    }
                    else
                    {
                        currentEvent++;
                        nextEventTimer = transitionTime;
                    }
                    break;

                case 2:
                    if (nextEventTimer > 0)
                    {
                        nextEventTimer -= Time.deltaTime;
                        Color fade = austinW.color;
                        fade.a = Mathf.Lerp(0, 1, nextEventTimer / transitionTime);
                        austinW.color = fade;
                    }
                    else
                    {
                        currentEvent++;
                        nextEventTimer = timePerScreen;
                    }
                    break;

                case 3:
                    if (nextEventTimer > 0)
                    {
                        nextEventTimer -= Time.deltaTime;
                        Color fade = chrisC.color;
                        fade.a = Mathf.Lerp(1, 0, nextEventTimer / transitionTime);
                        chrisC.color = fade;
                    }
                    else
                    {
                        currentEvent++;
                        nextEventTimer = timePerScreen;
                    }
                    break;

                case 5:
                    if (nextEventTimer > 0)
                    {
                        nextEventTimer -= Time.deltaTime;
                        Color fade = chrisC.color;
                        fade.a = Mathf.Lerp(0, 1, nextEventTimer / transitionTime);
                        chrisC.color = fade;
                    }
                    else
                    {
                        currentEvent++;
                        nextEventTimer = timePerScreen;
                    }
                    break;

                case 6:
                    if (nextEventTimer > 0)
                    {
                        nextEventTimer -= Time.deltaTime;
                        Color fade = marisaC.color;
                        fade.a = Mathf.Lerp(1, 0, nextEventTimer / transitionTime);
                        marisaC.color = fade;
                    }
                    else
                    {
                        currentEvent++;
                        nextEventTimer = timePerScreen;
                    }
                    break;

                case 8:
                    if (nextEventTimer > 0)
                    {
                        nextEventTimer -= Time.deltaTime;
                        Color fade = marisaC.color;
                        fade.a = Mathf.Lerp(0, 1, nextEventTimer / transitionTime);
                        marisaC.color = fade;
                    }
                    else
                    {
                        currentEvent++;
                        nextEventTimer = timePerScreen;
                        for (int i = 1; i < menu.Length; i++)
                        {
                            menu[i].GetComponent<BoxCollider>().enabled = true;
                        }
                    }
                    break;


                case 9:
                    if (nextEventTimer > 0)
                    {

                        nextEventTimer -= Time.deltaTime;
                        for (int i = 0; i < menu.Length; i++)
                        {
                            Color fade = menu[i].GetComponent<TextMesh>().color;
                            fade.a = Mathf.Lerp(1, 0, nextEventTimer / transitionTime);
                            menu[i].GetComponent<TextMesh>().color = fade;
                        }
                    }
                    else
                    {
                        currentEvent++;
                    }
                    break;
            }
        }
    }
}
