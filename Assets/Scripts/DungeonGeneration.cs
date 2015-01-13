using UnityEngine;
using System.Collections;

public class DungeonGeneration : MonoBehaviour
{
    //Fields
    //Public
    public Dungeon dungeonPrefab;

    //Private
    private Dungeon dungeonInstance;

    //Events
    // Use this for initialization
    void Start()
    {
        Begin();
    }

    //Methods
    private void Begin()
    {
        dungeonInstance = Instantiate(dungeonPrefab) as Dungeon;
        dungeonInstance.Generate();
    }

    public void NewLevel()
    {
        Destroy(dungeonInstance.gameObject);
        Begin();
    }
}
