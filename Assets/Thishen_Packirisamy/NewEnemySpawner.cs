using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;


public class NewEnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private GameObject[] enemySpawnPoints;
    public Wave[] waves;
    public float timeBetweenSpawns;
    public float timeBetweenWaves;
    private int currentWave = 0;
    private float timeSinceSpawn=0;
    private float timeSinceWave = 0;
    private bool changingWave = false;
    private bool waveSetup = false;
    private bool waveCleared;
    private int enemyCount = 0;
    private int previousEndValue = 0;
    private bool wavesCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        waveSetup = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (wavesCompleted==false)
        {
            if (changingWave == true)
            {
                
                GameObject[] Basicinfantries = GameObject.FindGameObjectsWithTag("BasicInfantry");
                GameObject[] FlyingEnemy = GameObject.FindGameObjectsWithTag("FlyingEnemy1");
                

                if (Basicinfantries.Length==0&& FlyingEnemy.Length==0)
                {
                    timeSinceWave += Time.deltaTime;
                    waveCleared = true;
                    
                }
            
                

                if (timeSinceWave>timeBetweenWaves)
                {
                    changingWave = false;
                    waveCleared = false;
                }
            }
            else
            {
                
                timeSinceSpawn += Time.deltaTime;
                if (timeSinceSpawn > timeBetweenSpawns)
                {
                    enemyCount = 0;
                    foreach (Enemy enemy in waves[currentWave].enemies)
                    {
                        enemyCount += enemy.amount;
                    }
                    Vector3 spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].transform.position;
                    int selector = Random.Range(0, enemyCount);
                 
                    previousEndValue = 0;
                    foreach (Enemy enemy in waves[currentWave].enemies)
                    {

                        if (selector < previousEndValue + enemy.amount && selector >= previousEndValue)
                        {
                          
                            Instantiate(enemyPrefabs[enemy.EnemyPrefabIndex], spawnPoint, Quaternion.identity);
                            enemy.amount--;
                            timeSinceSpawn = 0;
                            break;
                        }
                        else
                        {
                            previousEndValue += enemy.amount;
                        }
                    }

                    if (enemyCount<=0)
                    {
                       
                        changingWave = true;
                        SetUpWave();
                        waveSetup = false;
                        timeSinceSpawn = 0;
                    }

                }
            }
        }
        
       
    }

    private void SetUpWave()
    {
        int wave= currentWave++;
        if (wave>=waves.Length-1)
        {
            wavesCompleted = true;
        }
        else
        {
            enemyCount = 0;
            timeSinceSpawn = 0;
            timeSinceWave = 0;
            waveSetup = true;
        }
       
    }
}

