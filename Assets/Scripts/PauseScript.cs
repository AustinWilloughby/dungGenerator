using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseScript : MonoBehaviour
{
    //Fields
    //Public
    public bool drawing;

    //Private
    private List<GameObject> children;
    private InventoryManager inventory;

    // Use this for initialization
    void Start()
    {
        children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
        drawing = false;
        inventory = GameObject.Find("Player").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PauseGame()
    {
        if (Time.timeScale == 0f)
        {
            if (!inventory.Drawing)
            {
                Time.timeScale = 1f;
                GetComponent<Renderer>().enabled = false;
                drawing = false;
                foreach (GameObject child in children)
                {
                    child.SetActive(false);
                }
            }
            else
            {
                GetComponent<Renderer>().enabled = true;
                drawing = true;
                foreach (GameObject child in children)
                {
                    child.SetActive(true);
                }
            }
        }
        else
        {
            Time.timeScale = 0;
            GetComponent<Renderer>().enabled = true;
            drawing = true;
            foreach (GameObject child in children)
            {
                child.SetActive(true);
            }
        }


    }
}
