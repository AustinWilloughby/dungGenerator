using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour
{
    //Fields
    //Public
    public AudioSource[] backgroundMusic;
    public AudioSource roomTone;

    //Private
    private GameObject pause;
    private bool lastFramePause;
    private int songIndex;


    // Use this for initialization
    void Start()
    {
        songIndex = 1;
        lastFramePause = true;
        pause = GameObject.Find("PauseMenu");
    }

    // Update is called once per frame
    void Update()
    {
        PlayMusic();
        PlayRoomTone();
    }

    //Methods
    private void PlayMusic()
    {
        if (pause.renderer.enabled)
        {
            if (backgroundMusic[songIndex].isPlaying)
            {
                backgroundMusic[songIndex].Pause();
            }
        }
        if (lastFramePause && !pause.renderer.enabled)
        {
            backgroundMusic[songIndex].Play();
        }

        lastFramePause = pause.renderer.enabled;
    }

    private void PlayRoomTone()
    {
        if (!roomTone.isPlaying)
        {
            if (Random.Range(0, 100) == 0)
            {
                roomTone.Play();
            }
        }
    }
}
