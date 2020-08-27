using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float timeBetweenSpawns;
    public int amountOfEnemiesPerSpawn;
    private float timeSinceSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn>timeBetweenSpawns)
        {
            spawn(amountOfEnemiesPerSpawn);   
        }
    }


    private void spawn(int amountPerSpawn)
    {
       
        List<Vector3> PossibleSpawns = new List<Vector3>();

        PossibleSpawns =  GameController.instance.GetSpawnPos();
        if (PossibleSpawns != null)
        {
            for (int i = 0; i < amountPerSpawn; i++)
            {
                Vector3 spawnPos = PossibleSpawns[Random.Range(0, PossibleSpawns.Count)];
                //int enemySelector = Random.Range(0, 100);
                Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnPos, Quaternion.identity);

            }
            
        }
        timeSinceSpawn = 0;
    }

}
