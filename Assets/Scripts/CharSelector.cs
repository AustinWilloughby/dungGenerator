using UnityEngine;
using System.Collections;

public class CharSelector : MonoBehaviour
{
    //Fields
    //Public
    public PlayerClass button;

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
    }

    void OnMouseEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
    }

    void OnMouseUpAsButton()
    {
        //If button is a
        switch(button)
        {
            case PlayerClass.Adventurer:
                //Load adventurer level
                Application.LoadLevel(4);
                break;

            case PlayerClass.Assassin:
                //Load assassin level
                Application.LoadLevel(2);
                break;

            default:
                //Load knight level
                Application.LoadLevel(3);
                break;
        }
    }
}
