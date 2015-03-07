using UnityEngine;
using System.Collections;

public class MusicStarter : MonoBehaviour
{
    //Fields
    //Public
    public AudioSource music;


    // Use this for initialization
    void Start()
    {
        music.Play();
    }
}
