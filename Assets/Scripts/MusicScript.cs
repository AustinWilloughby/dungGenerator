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
    private float songTimer;


    // Use this for initialization
    void Start()
    {
        songTimer = Random.Range(30, 50);
        songIndex = 0;
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
    private void PlayMusic() //handles music, and when it should be playing
    {
        if (lastFramePause && !pause.GetComponent<Renderer>().enabled)
        {
            backgroundMusic[songIndex].Play();
        }

        if (pause.GetComponent<Renderer>().enabled)
        {
            if (backgroundMusic[songIndex].isPlaying)
            {
                backgroundMusic[songIndex].Pause();
            }
        }
        else
        {
            if (!backgroundMusic[songIndex].isPlaying)
            {
                if (songTimer >= 0)
                {
                    songTimer -= Time.deltaTime;
                }
                else
                {
                    songTimer = Random.Range(30, 50);
                    backgroundMusic[songIndex].Stop();
                    backgroundMusic[songIndex].Play();
                }
            }
        }

        lastFramePause = pause.GetComponent<Renderer>().enabled;
    }

    private void PlayRoomTone() //Plays roomtone randomly if it is not playing
    {
        if (!roomTone.isPlaying)
        {
            if (Random.Range(0, 100) == 0)
            {
                roomTone.Play();
            }
        }
    }

    public void NextSong() //Goes to next song
    {
        backgroundMusic[songIndex].Stop();
        songIndex = (songIndex + 1) % backgroundMusic.Length;
        backgroundMusic[songIndex].Play();
    }
}
