using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseScript : MonoBehaviour
{
    //Fields
    //Private
    private List<GameObject> children;

    // Use this for initialization
    void Start()
    {
        children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PauseGame()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            renderer.enabled = false;
            foreach (GameObject child in children)
            {
                child.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 0f;
            renderer.enabled = true;
            foreach (GameObject child in children)
            {
                child.SetActive(true);
            }
        }
    }
}
