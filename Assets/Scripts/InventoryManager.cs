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
    private bool drawing;


    //Properties
    public int CoinCount
    {
        get { return coinCount; }
    }


    //Events
    // Use this for initialization
    void Start()
    {
        drawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            drawing = !drawing;
        }
    }

    public void OnGUI()
    {
        if (drawing)
        {
            GUI.DrawTexture(new Rect(Screen.width - 300, Screen.height - 150, 300, 150), bag, ScaleMode.ScaleToFit);
        }
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
