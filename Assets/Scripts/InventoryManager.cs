using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    //Fields
    //Public

    //Private
    private int coinCount;
    private GameObject[] inventory;
    private int[] objectCount;


    //Properties
    public int CoinCount
    {
        get { return coinCount; }
    }


    //Events
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //Methods
    public void AddCoinValue(int addValue)
    {
        if (addValue >= 0)
        {
            coinCount += addValue;
        }
        else
        {
            coinCount -= addValue;
        }
    }
}
