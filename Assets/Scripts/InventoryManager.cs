using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    //Fields
    //Public
    public Texture coin;
    public Texture rope;
    public Texture potion;
    public Texture bag;

    //Private
    private int coinCount;
    private int potionCount;
    private bool ropeCollected;


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

    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - 300, Screen.height - 150, 300, 150), bag, ScaleMode.ScaleToFit);
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
