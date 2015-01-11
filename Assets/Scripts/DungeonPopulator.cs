using UnityEngine;
using System.Collections;

public class DungeonPopulator : MonoBehaviour
{
    //Fields
    //Public
    public GameObject ropePrefab;

    //Private
    private Dungeon dungeon;
    private GameObject player; 
    private GameObject ropelessHole;


    //Methods
    public void Populate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ropelessHole = GameObject.Find("EmptyDungeonHole");
        dungeon = gameObject.GetComponent<Dungeon>();

        PlaceRopeAndHole();
    }


    private void PlaceRopeAndHole()
    {
        do
        {
            int xLoc = (int)Random.Range(0, dungeon.size.x) * dungeon.cellScale;
            int yLoc = (int)Random.Range(0, dungeon.size.y) * dungeon.cellScale;
            ropelessHole.transform.position = new Vector3(xLoc, yLoc, 30);
        } while (Vector2.Distance((Vector2)ropelessHole.transform.position, (Vector2)player.transform.position) < 75);

        bool looper = true;
        GameObject rope = (GameObject)Instantiate(ropePrefab);
        do
        {
            int xLoc = (int)Random.Range(0, dungeon.size.x) * dungeon.cellScale;
            int yLoc = (int)Random.Range(0, dungeon.size.y) * dungeon.cellScale;
            rope.transform.position = new Vector3(xLoc, yLoc);

            bool playerDist = false;
            bool holeDist = false;
            if (Vector2.Distance((Vector2)rope.transform.position, (Vector2)player.transform.position) > 50)
            {
                playerDist = true;
            }
            if (Vector2.Distance((Vector2)rope.transform.position, (Vector2)ropelessHole.transform.position) > 50)
            {
                holeDist = true;
            }
            if (holeDist && playerDist)
            {
                looper = false;
            }
        } while (looper);
    }



    private DungeonCell GetPlayerCell()
    {
        IntVector2 playerCoords = new IntVector2(((int)player.transform.position.x) / dungeon.cellScale,
                ((int)player.transform.position.y) / dungeon.cellScale);

        DungeonCell playerCell = null;
        if (dungeon.ContainsCoords(playerCoords))
        {
            playerCell = dungeon.GetCell(playerCoords);
        }

        return playerCell;
    }


}
