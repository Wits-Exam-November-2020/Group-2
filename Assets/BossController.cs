using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Animator anim;
    public GameObject flyingEnemy;
    private float timer=0;
    private float timeSinceSpawn = 0;
    public float timeBetweenSpawnEvents=4;
    public float timeBetweenSpawns = 1;
    public int amountToSpawn=1;
    private int currentAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentAmount = amountToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > timeBetweenSpawnEvents)
        {
            
            anim.SetBool("open",true);
            timer += Time.deltaTime;
            if (timer>timeBetweenSpawns&&currentAmount>0)
            {
                currentAmount--;
                timer = 0;
                Instantiate(flyingEnemy, transform.position+ new Vector3(0,2.8f,0), Quaternion.identity);
            }
            if (currentAmount == 0)
            {
                currentAmount = amountToSpawn;
                anim.SetBool("open", false);
                timeSinceSpawn = 0;
                timer = timeBetweenSpawns;
            }
        }
        
    }


    private void SpawnDrones()
    {

    }
}
