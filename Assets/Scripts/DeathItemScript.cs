using UnityEngine;
using System.Collections;

public class DeathItemScript : MonoBehaviour
{

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
        switch (gameObject.name)
        {
            case "MainMenu":
                Application.LoadLevel(5);
                break;

            case "Exit":
                Application.Quit();
                break;
        }
    }
}
