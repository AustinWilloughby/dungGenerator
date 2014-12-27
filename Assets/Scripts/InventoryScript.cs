using UnityEngine;
using System.Collections;
using System;

public class InventoryScript : MonoBehaviour
{
    //Fields
    //Public
    public int initialSlots = 4;

    //Private
    private GameObject player;
    private GameObject[] bagInventory;
    private GameObject[] wearInventory;
    private int currentBagTotal = 0;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bagInventory = new GameObject[initialSlots];
        wearInventory = new GameObject[1];
    }

    public void AddToBagInventory(GameObject newItem)
    {
        if (currentBagTotal < bagInventory.Length)
        {
            for (int i = 0; i < bagInventory.Length; i++)
            {
                if (bagInventory[i] == null)
                {
                    bagInventory[i] = newItem;
                    break;
                }
            }
        }
    }

    public void RemoveFromBagInventory(GameObject removeItem)
    {
        for (int i = 0; i < bagInventory.Length; i++)
        {
            if (bagInventory[i] == removeItem)
            {
                bagInventory[i] = null;
            }
        }
    }
}
