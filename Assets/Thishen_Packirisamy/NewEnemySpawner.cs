using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewEnemySpawner : MonoBehaviour
{
    public Enemies[] enemies;
    public Wave[] waves;
    public float timeBetweenSpawns;
    private float timeSinceSpawn=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceSpawn>timeBetweenSpawns)
        {

        }
    } 
}

[System.Serializable]
public class Wave
{
    public Enemies[] enemies;
}
[System.Serializable]
public class Enemies 
{
    public int amount;
    public int EnemyIndex;
}
