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
    private GameObject ropelessHole;
    private GameObject ropeHole;
    private Vector3 holdingCell = new Vector3(-20, -20, 30);
    private GameObject player;
    private PauseScript pause;
    private FxHandler soundFX;


    //Properties
    public int CoinCount
    {
        get { return coinCount; }
    }
    public bool Drawing
    {
        get { return drawing; }
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
        pause = GameObject.Find("PauseMenu").GetComponent<PauseScript>();
        soundFX = GameObject.Find("Main Camera").GetComponent<FxHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pause.gameObject.renderer.enabled == true)
        {
            drawing = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                drawing = !drawing;
                if (drawing)
                {
                    soundFX.bagSound.Play();
                }
            }
        }
        if (drawing || pause.drawing)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void OnGUI()
    {
        //Text styling
        GUIStyle style = new GUIStyle();
        style.fontSize = 40;
        style.normal.textColor = Color.white;
        GUI.backgroundColor = Color.clear;

        if (drawing)//If the inventory is drawing
        {
            //Draw base
            GUI.DrawTexture(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 133, 400, 266), bag, ScaleMode.ScaleToFit);

            //Draw potions if there are any
            if (potionCount > 0)
            {
                GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 40, 400, 266), "x" + potionCount.ToString(), style);
                Rect r = new Rect(Screen.width / 2 - 170, Screen.height / 2 - 35, 125, 125);
                GUI.DrawTexture(r, potion, ScaleMode.ScaleToFit);
                if (r.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0)) //Handles player input
                {
                    if (player.GetComponent<StatTracker>().health < player.GetComponent<StatTracker>().MaxHealth)
                    {
                        soundFX.potionSound.Play();
                        GameObject.Find("Player").GetComponent<StatTracker>().ApplyHealing(5);
                        potionCount--;
                    }
                }

            }

            if (ropeCollected)//If the rope has been collected
            {
                Rect r = new Rect(Screen.width / 2 + 30, Screen.height / 2 - 35, 125, 125);
                GUI.DrawTexture(r, rope, ScaleMode.ScaleToFit);
                if (r.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0)) //Handles Player input
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

            //Draw coin display
            GUI.DrawTexture(new Rect(Screen.width / 2 - 170, Screen.height / 2 - 105, 30, 30), coin, ScaleMode.ScaleToFit);
            GUI.DrawTexture(new Rect(Screen.width / 2 + 140, Screen.height / 2 - 105, 30, 30), coin, ScaleMode.ScaleToFit);
            style.fontSize = 30;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(Screen.width / 2 - 12.5f, Screen.height / 2 - 103, 25, 25), coinCount.ToString(), style);
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
