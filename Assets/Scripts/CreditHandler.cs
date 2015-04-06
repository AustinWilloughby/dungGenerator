using UnityEngine;
using System.Collections;

public class CreditHandler : MonoBehaviour
{
    //Fields
    //Public
    public float scrollSpeed;
    public float totalLength;
    
    //Private
    private AudioSource music;

    // Use this for initialization
    void Start()
    {
        music = GameObject.Find("bMusic 2").GetComponent<AudioSource>();
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translate = transform.position;
        translate.y += scrollSpeed * Time.deltaTime;
        transform.position = translate;

        //If the credits are done or the player hits escape
        if (translate.y > totalLength + 4 || Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel(5);
        }

        //Begin to fade music
        if (translate.y > totalLength)
        {
            music.volume = Mathf.Lerp(.25f, 0, ((translate.y - totalLength) / 4));
        }
    }
}
