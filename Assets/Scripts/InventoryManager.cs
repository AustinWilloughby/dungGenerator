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
    public bool ropeCollected;
    public int potionCount;

    //Private
    private int coinCount;
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
        ropeCollected = false;
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
            if (potionCount > 0)
            {
                GUI.DrawTexture(new Rect(Screen.width - 265, Screen.height - 90, 65, 65), potion, ScaleMode.ScaleToFit);
            }
            if (ropeCollected)
            {
                GUI.DrawTexture(new Rect(Screen.width - 100, Screen.height - 90, 65, 65), rope, ScaleMode.ScaleToFit);
            }
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
