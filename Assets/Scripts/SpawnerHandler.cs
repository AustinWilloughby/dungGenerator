using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerHandler : MonoBehaviour
{
    //Fields
    //Public
    public GameObject enemyPrefab; //The enemy the spawner will spawn
    public float spawnRate = 1; //The minimum time between enemy spawns
    public int maxSpawns = 1; //Maximum number of enemies alive from one spawner at a time
    public GameObject player;

    //Private
    private float timeUntilSpawn; //A timer counting down until the next spawn
    private int liveSpawns = 0; //Current number of enemies alive from the spawner


    //Use this for instantiation
    void Start()
    {
        timeUntilSpawn = Random.Range(5f, 15f);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) < 35f)
        {
            if (Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) > 20f)
            {
                if (liveSpawns < maxSpawns)
                {
                    if (timeUntilSpawn <= 0) //If its time to spawn
                    {
                        timeUntilSpawn = Random.Range(spawnRate, spawnRate * 1f);

                        liveSpawns++;
                        GameObject tempEnemy = (GameObject)Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                        tempEnemy.GetComponent<EnemyScript>().spawner = this;
                        tempEnemy.transform.parent = transform;
                    }
                    timeUntilSpawn -= Time.deltaTime; //Counts down till time to spawn
                }
            }
        }
    }


    //Methods
    public void KillSpawn() //Removes one from spawn count
    {
        if (liveSpawns > 0)
        {
            liveSpawns--;
        }
    }
}
