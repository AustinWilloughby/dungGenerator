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

    //Private
    private float timeUntilSpawn; //A timer counting down until the next spawn
    private int liveSpawns = 0; //Current number of enemies alive from the spawner


    //Use this for instantiation
    void Start()
    {
        timeUntilSpawn = 0;
    }
	
	// Update is called once per frame
    void Update () 
    {
        if (liveSpawns < maxSpawns)
        {
            if (timeUntilSpawn <= 0) //If its time to spawn
            {
                timeUntilSpawn = Random.Range(spawnRate, spawnRate * 1f);

                liveSpawns++;
                GameObject tempEnemy = (GameObject)Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                tempEnemy.GetComponent<EnemyScript>().spawner = this;
            }
            timeUntilSpawn -= Time.deltaTime; //Counts down till time to spawn
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
