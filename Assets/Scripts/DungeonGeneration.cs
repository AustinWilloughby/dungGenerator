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
    private GameObject playerSpawn;
    private GameObject player;

    //Events
    // Use this for initialization
    void Start()
    {
        playerSpawn = GameObject.Find("PlayerSpawn");
        player = GameObject.Find("Player");
        dungeonLevel = 0;
        Begin();
    }

    //Methods
    private void Begin()
    {
        if(dungeonLevel > 10)
        {
            dungeonLevel++;
        }
        dungeonInstance = Instantiate(dungeonPrefab) as Dungeon;
        dungeonInstance.dungeonLevel = dungeonLevel;
        dungeonInstance.Generate();
        if (dungeonLevel == 1)
        {
            IntVector2 coords = dungeonInstance.RandomCoordinates;
            player.transform.position = new Vector3(coords.x * dungeonInstance.cellScale, coords.y * dungeonInstance.cellScale, 0);
            camera.transform.position = new Vector3(coords.x * dungeonInstance.cellScale, coords.y * dungeonInstance.cellScale, -8);
            playerSpawn.transform.position = player.transform.position;
        }
    }

    public void NewLevel()
    {
        Destroy(dungeonInstance.gameObject);
        player.transform.position = (Vector2)playerSpawn.transform.position;
        Begin();
    }
}
