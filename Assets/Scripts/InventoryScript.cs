using UnityEngine;
using System.Collections;
using System;

public class InventoryScript : MonoBehaviour
{
    //Fields
    //Public
    public int initialSlots = 4;
    public int purseValue = 0;

    //Private
    private GameObject[] bagInventory;
    private GameObject[] wearInventory;
    private int currentBagTotal = 0;

    // Use this for initialization
    void Start()
    {
        bagInventory = new GameObject[initialSlots];
        wearInventory = new GameObject[6];
    }

    //Methods
    public void AddToBagInventory(GameObject newItem) //Attempts to add an item to your bag inventory
    {
        //If there is space
        if (currentBagTotal < bagInventory.Length)
        {
            for (int i = 0; i < bagInventory.Length; i++)
            {
                //First available slot
                if (bagInventory[i] == null)
                {
                    bagInventory[i] = newItem;
                    break;
                }
            }
        }
    }

    public GameObject RemoveFromBagInventory(GameObject removeItem) //Attempts to remove an item from your bag inventory
    {
        for (int i = 0; i < bagInventory.Length; i++)
        {
            if (bagInventory[i] == removeItem)
            {
                bagInventory[i] = null;
            }
        }
        return removeItem;
    }

    public GameObject AutoEquipItem(GameObject equipItem) //Attempts to equipt an item automatically;
    {
        if (equipItem.tag == "Equipable")
        {
            switch (equipItem.GetComponent<EquipIdentifier>().slot)
            {
                case EquipSlots.Head:
                    if (wearInventory[0] == null)
                    {
                        wearInventory[0] = equipItem;
                    }
                    return null;
                case EquipSlots.Shoulder:
                    if (wearInventory[1] == null)
                    {
                        wearInventory[1] = equipItem;
                    }
                    return null;
                case EquipSlots.Chest:
                    if (wearInventory[2] == null)
                    {
                        wearInventory[2] = equipItem;
                    }
                    return null;
                case EquipSlots.Pants:
                    if (wearInventory[3] == null)
                    {
                        wearInventory[3] = equipItem;
                    }
                    return null;
                case EquipSlots.Weapon:
                    if (wearInventory[4] == null)
                    {
                        wearInventory[4] = equipItem;
                    }
                    return null;
                case EquipSlots.Ranged:
                    if (wearInventory[5] == null)
                    {
                        wearInventory[5] = equipItem;
                    }
                    return null;
                default: return equipItem;
            }
        }
        return equipItem;
    }
}
