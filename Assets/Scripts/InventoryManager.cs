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
    private float potionTimer;
    private GameObject ropelessHole;
    private GameObject ropeHole;
    private Vector3 holdingCell = new Vector3(-20, -20, 30);
    private GameObject player;


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
        ropelessHole = GameObject.Find("EmptyDungeonHole");
        ropeHole = GameObject.Find("DungeonHole");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            drawing = !drawing;
        }
        if (potionTimer > 0)
        {
            potionTimer -= Time.deltaTime;
        }
    }

    public void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;
        GUI.backgroundColor = Color.clear;
        if (drawing)
        {
            GUI.DrawTexture(new Rect(Screen.width - 300, Screen.height - 150, 300, 150), bag, ScaleMode.ScaleToFit);
            if (potionCount > 0)
            {
                GUI.Label(new Rect(Screen.width - 258, Screen.height - 90, 65, 65), potionCount.ToString(), style);
                Rect r = new Rect(Screen.width - 260, Screen.height - 90, 65, 65);
                GUI.DrawTexture(r, potion, ScaleMode.ScaleToFit);
                if (r.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0))
                {
                    if (potionTimer <= 0 && (player.GetComponent<StatTracker>().health < player.GetComponent<StatTracker>().MaxHealth))
                    {
                        GameObject.Find("Player").GetComponent<StatTracker>().ApplyHealing(5);
                        potionCount--;
                        potionTimer = 1;
                    }
                }

            }
            if (ropeCollected)
            {
                Rect r = new Rect(Screen.width - 100, Screen.height - 90, 65, 65);
                GUI.DrawTexture(r, rope, ScaleMode.ScaleToFit);
                if (r.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0))
                {
                    if (Vector2.Distance(player.transform.position, ropelessHole.transform.position) < 5)
                    {
                        ropeCollected = false;
                        Vector3 holePos = ropelessHole.transform.position;
                        ropelessHole.transform.position = holdingCell;
                        ropeHole.transform.position = holePos;
                    }
                }
            }
            GUI.DrawTexture(new Rect(Screen.width - 190, Screen.height - 136, 25, 25), coin, ScaleMode.ScaleToFit);
            GUI.Label(new Rect(Screen.width - 165, Screen.height - 135, 25, 25), " x " + coinCount.ToString(), style);
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
