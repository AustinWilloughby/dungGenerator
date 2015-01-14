using UnityEngine;
using System.Collections;

public class DungeonGeneration : MonoBehaviour
{
    //Fields
    //Public
    public Dungeon dungeonPrefab;

    //Private
    private Dungeon dungeonInstance;
    private int dungeonLevel;

    //Events
    // Use this for initialization
    void Start()
    {
        dungeonLevel = 0;
        Begin();
    }

    //Methods
    private void Begin()
    {
        dungeonLevel++;
        dungeonInstance = Instantiate(dungeonPrefab) as Dungeon;
        dungeonInstance.dungeonLevel = dungeonLevel;
        dungeonInstance.Generate();
    }

    public void NewLevel()
    {
        Destroy(dungeonInstance.gameObject);
        Begin();
    }
}
