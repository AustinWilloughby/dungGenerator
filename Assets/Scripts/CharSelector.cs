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
        switch(button)
        {
            case PlayerClass.Adventurer:
                Application.LoadLevel(4);
                break;

            case PlayerClass.Assassin:
                Application.LoadLevel(2);
                break;

            default:
                Application.LoadLevel(3);
                break;
        }
    }
}
