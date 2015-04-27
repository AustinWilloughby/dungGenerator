using UnityEngine;
using System.Collections;

public class CharSelector : MonoBehaviour
{
    //Fields
    //Public
    public PlayerClass button;
    
    //Private
    private GameObject text;

    void Start() //Default color
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
        text = gameObject.transform.GetChild(0).gameObject;
        text.SetActive(false);
    }

    void OnMouseEnter() //Mouse over to white
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        text.SetActive(true);
    }

    void OnMouseExit() //No mouse over to gray
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
        text.SetActive(false);
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
