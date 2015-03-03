using UnityEngine;
using System.Collections;

public class PauseMenuItem : MonoBehaviour
{
    //Fields
    //Private
    private GameObject pauseMenu;

    //Events
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
    }

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
        pauseMenu.GetComponent<PauseScript>().PauseGame();
        switch (gameObject.name)
        {
            case "MainMenu":
                Application.LoadLevel(0);
                break;

            case "Options":
                break;

            case "Exit":
                Application.Quit();
                break;
        }
    }
}
