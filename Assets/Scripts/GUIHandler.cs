using UnityEngine;
using System.Collections;

public class GUIHandler : MonoBehaviour
{
    //Fields
    //Public
    public Texture2D[] heartTypes;

    //Private
    private StatTracker playerStats;

    // Use this for initialization
    void Start()
    {
        playerStats = gameObject.GetComponent<StatTracker>();
    }

    void OnGUI()
    {
        DrawHearts();
    }

    private void DrawHearts()
    {
                //Draws players health to the screen
        int heartsDrawn = 0;
        for (int i = 0; i < playerStats.health / 2; i++)
        {
            GUI.DrawTexture(new Rect(10 + 65 * heartsDrawn, 10, 60, 60), heartTypes[0], ScaleMode.StretchToFill, true);
            heartsDrawn++;
        }
        if (playerStats.health / 2 < ((float)(playerStats.health) / 2f))
        {
            GUI.DrawTexture(new Rect(10 + 65 * heartsDrawn, 10, 60, 60), heartTypes[1], ScaleMode.StretchToFill, true);
            heartsDrawn++;
        }
        int emptyHearts = playerStats.MaxHealth - playerStats.health;
        if((emptyHearts)%2 == 1)
        {
            emptyHearts--;
        }
        for (int i = 0; i < emptyHearts / 2; i++)
        {
            GUI.DrawTexture(new Rect(10 + 65 * heartsDrawn, 10, 60, 60), heartTypes[2], ScaleMode.StretchToFill, true);
            heartsDrawn++;
        }
    }
}