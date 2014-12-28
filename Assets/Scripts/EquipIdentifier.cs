using UnityEngine;
using System.Collections;

public enum EquipSlots
{
    Unidentified,
    Head,
    Shoulder,
    Chest,
    Pants,
    Weapon,
    Ranged
};


public class EquipIdentifier : MonoBehaviour
{
    //Identifies what kind of item the equipable item is
    public EquipSlots slot;
}
