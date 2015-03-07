﻿using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
    //Fields
    //Public
    public MenuElement button;

    //Events
    void OnMouseEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseUpAsButton()
    {
        switch (button)
        {
            case MenuElement.Options:
                break;

            case MenuElement.Start:
                Application.LoadLevel(1);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

public enum MenuElement
{
    Start,
    Options,
    Credits
};
