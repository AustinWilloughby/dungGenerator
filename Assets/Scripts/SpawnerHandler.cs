using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerHandler : MonoBehaviour 
{
    //Fields
    public GameObject enemyPrefab; //The enemy the spawner will spawn
    public float spawnRate = 1; //The minimum time between enemy spawns
    private List<GameObject> enemies; //A list of all living enemies spawned
    private float timeUntilSpawn; //A timer counting down until the next spawn

    //Use this for instantiation
    void Start()
    {
        timeUntilSpawn = 0;
        enemies = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (timeUntilSpawn <= 0) //If its time to spawn
        {
            timeUntilSpawn = Random.Range(spawnRate, spawnRate * 1.1f);

            GameObject tempEnemy = (GameObject)Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemies.Add(tempEnemy);
        }
        timeUntilSpawn -= Time.deltaTime; //Counts down till time to spawn
	}
}
