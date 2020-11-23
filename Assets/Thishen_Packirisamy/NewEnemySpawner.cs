using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NewEnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private List<GameObject> enemySpawnPoints;
    public Wave[] waves;
    public GameObject boss;
    public float timeBetweenSpawns;
    public float timeBetweenWaves;
    public int currentWave = 0;
    private float timeSinceSpawn = 0;
    private float timeSinceWave = 0;
    private bool changingWave = false;
    private bool waveSetup = false;
    private bool waveCleared;
    private int enemyCount = 0;
    private int previousEndValue = 0;
    private bool wavesCompleted = false;
    private GameObject player;
    private Text waveText;
    private float textTime=0;

    private bool shownText = false;
    private bool waiting = false;

    // Start is called before the first frame update
    void Start()
    {
        waveText = GameObject.Find("HUD/WaveText").GetComponent<Text>();
        ShowWaveText("Wave 1");
    }

    // Update is called once per frame
    void Update()
    {
        //REMOVE AFTER DRONE FIX
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject[] FlyingEnemys = GameObject.FindGameObjectsWithTag("FlyingEnemy1");
            foreach (GameObject stuckEnemy in FlyingEnemys)
            {
                Destroy(stuckEnemy);
            }
        }

        player = GameObject.Find("Player(Clone)");
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint").ToList<GameObject>();
        waveSetup = true;
        if (wavesCompleted ==true)
        {
            GameObject[] Basicinfantries = GameObject.FindGameObjectsWithTag("BasicInfantry");
            GameObject[] FlyingEnemy = GameObject.FindGameObjectsWithTag("FlyingEnemy1");
            if (Basicinfantries.Length == 0 && FlyingEnemy.Length == 0 && shownText==false)
            {
                    ShowWaveText("Boss Round");
                    shownText = true;
                    boss.SetActive(true);
            }

        }
        if (wavesCompleted == false)
        {
            if (changingWave == true)
            {

                GameObject[] Basicinfantries = GameObject.FindGameObjectsWithTag("BasicInfantry");
                GameObject[] FlyingEnemy = GameObject.FindGameObjectsWithTag("FlyingEnemy1");
                


                if (Basicinfantries.Length == 0 && FlyingEnemy.Length == 0&& waiting==false)
                {
                    
                    ShowWaveText("Wave " + currentWave + " Cleared");
                    
                    waveCleared = true;
                    waiting = true;
                }
                if (waiting)
                {
                    timeSinceWave += Time.deltaTime;
                }



                if (timeSinceWave > timeBetweenWaves)
                {
                    ShowWaveText("Wave " + (currentWave + 1));
                    changingWave = false;
                    waveCleared = false;
                    waiting = false;
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
                    
                    enemySpawnPoints.Sort(SortByDistance);

                    int randomLimit;
                    if (enemySpawnPoints.Count<4)
                    {
                        randomLimit = enemySpawnPoints.Count;
                    }
                    else
                    {
                        randomLimit = 4;
                    }
                    
                    Vector3 spawnObjectPos = enemySpawnPoints[Random.Range(1, randomLimit)].transform.position;
                    //Vector3 spawnPoint = new Vector3(spawnObjectPos.x,20,spawnObjectPos.z);
                    Vector3 spawnPoint = spawnObjectPos;


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

                    if (enemyCount <= 0)
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

  



    int SortByDistance(GameObject point1, GameObject point2)
    {
        float dist1 = Vector3.Distance(point1.transform.position,player.transform.position);
        float dist2 = Vector3.Distance(point2.transform.position, player.transform.position);
        return dist1.CompareTo(dist2);
    }

    private void ShowWaveText(string text)
    {
        
        waveText.text = text;
        waveText.GetComponent<Animator>().SetTrigger("ShowText");
       
    }
    private void SetUpWave()
    {
       
        int wave = currentWave++;
        if (wave >= waves.Length - 1)
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

