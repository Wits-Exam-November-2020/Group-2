using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject cog;
    public GameObject chip1;
    public GameObject chip2;
    public int damage = 100;
    public float rotationSpeed = 0.5f;
    private float moveSpeed = 5f;
    public float startSpeed = 3f;
    public float raycastOffset = 1;
    public float rayDistance;
    public float turnSpeed;
    private GameObject player;

    public NavMeshAgent enemy;
    private Transform target;
    public float health;
    public float maxHealth;
    private List<GameObject> enemySpawnPoints;
    private bool isAttacking = false;

    public AudioSource hit1Sound;
    public AudioSource hit2Sound;
    public AudioSource hit3Sound;

    public int attackRate;

    private bool movingDown = false;

    public GameObject deathSoundController;
    public GameObject enemyDeathExplosion;
    private float stuckCheckTime = 0;
    private bool moved=false;
    private Vector3 startPosStuckCheck;
    public float stuckDistance=5;

    private void Start()
    { 
        health = maxHealth;
        moveSpeed = startSpeed;
        startPosStuckCheck = transform.position;
            
        
    }


    void Update()
    {

        player = GameObject.Find("Player(Clone)");
        target = GameObject.Find("Player(Clone)").transform;


        if (transform.position.y>50 && !movingDown)
        {
            movingDown = true;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.transform.rotation = Quaternion.identity;
        }

        if (transform.position.y < 50 && movingDown)
        {
            movingDown = false;
        }


        ////point();
        if (gameObject.tag == "FlyingEnemy1")
        {

            if (Vector3.Distance(transform.position, player.transform.position) > 8)
            {
                stuckCheckTime += Time.deltaTime;
               
                if ((int)stuckCheckTime%2==0)
                {
                    Debug.Log("checking");
                    if (Vector3.Distance(transform.position,startPosStuckCheck)>stuckDistance)
                    {
                        moved = true;
                        Debug.Log("moved");
                    }
                    if (stuckCheckTime >= 10)
                    {
                        if (moved == false)
                        {
                            Debug.Log("respawn");
                            enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint").ToList<GameObject>();
                            enemySpawnPoints.Sort(SortByDistance);
                            transform.position = enemySpawnPoints[Random.Range(1, enemySpawnPoints.Count - 1)].transform.position;
                        }
                        startPosStuckCheck = transform.position;
                        moved = false;
                        stuckCheckTime = 0;
                    }
                }
            }
            rays();
            if (Vector3.Distance(transform.position, player.transform.position) > 5)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                //this.GetComponent<Rigidbody>().velocity = moveSpeed * transform.forward;
            }
            else
            {

                //this.GetComponent<Rigidbody>().velocity
                if (!isAttacking)
                {
                    isAttacking = true;
                    player.GetComponent<Player>().TakeDamage(damage);
                    Invoke("ResetAttack", attackRate);


                }
            }
        }
        else
        {

            enemy = this.GetComponent<NavMeshAgent>();
            enemy.SetDestination(target.position);
            if (Vector3.Distance(enemy.transform.position, target.position) <= enemy.stoppingDistance)
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    player.GetComponent<Player>().TakeDamage(damage);
                    Invoke("ResetAttack", attackRate);


                }
                Debug.Log("Attack");
            }
    }











}

    private void point()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y+2, player.transform.position.z);
        Vector3 dir = targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void rays()
    {
        Vector3 turn = Vector3.zero;
        RaycastHit hit;
        Vector3 left = transform.position - transform.right * raycastOffset;
        Vector3 right = transform.position + transform.right * raycastOffset;
        Vector3 up = transform.position + transform.up * raycastOffset;
        Vector3 down = transform.position - transform.up * raycastOffset;

        Debug.DrawRay(left, transform.forward * rayDistance,Color.cyan);
        Debug.DrawRay(right, transform.forward * rayDistance, Color.cyan);
        Debug.DrawRay(up, transform.forward * rayDistance, Color.cyan);
        Debug.DrawRay(down, transform.forward * rayDistance, Color.cyan);

        if (Physics.Raycast(left, transform.forward, out hit, rayDistance))
        {
            turn += Vector3.up;

        }else if(Physics.Raycast(right, transform.forward, out hit, rayDistance))
        {
            turn += Vector3.down;
         
        }
        else if (Physics.Raycast(down, transform.forward, out hit, rayDistance))
        {
            turn -= Vector3.right;
            
        }
        else if (Physics.Raycast(up, transform.forward, out hit, rayDistance))
        {
            turn += Vector3.left;
           
        }


        if (turn != Vector3.zero )
        {
            if (hit.collider != null)
            {
                //if (hit.collider.tag == "Building")
                //{
                //    moveSpeed = hit.distance - 0.5f;
                //    transform.Rotate(turn * turnSpeed * Time.deltaTime);
                //}

                if (hit.collider.tag == "FlyingEnemy1")
                {
                        moveSpeed = hit.distance - 0.5f;
                        transform.Rotate(turn * turnSpeed * Time.deltaTime);
               // transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 0.5f*Time.deltaTime);

                }
                else
                {
                    if ((hit.distance - 1f) > 0)
                    {
                        moveSpeed = hit.distance - 1f;
                    }

                    transform.Rotate(turn * turnSpeed * Time.deltaTime);
                }
            }
            
        }
        else
        {
            moveSpeed = startSpeed;
            point();
        }
    }

    public void ResetAttack()
    {
        isAttacking = false;
    }
    int SortByDistance(GameObject point1, GameObject point2)
    {
        float dist1 = Vector3.Distance(point1.transform.position, player.transform.position);
        float dist2 = Vector3.Distance(point2.transform.position, player.transform.position);
        return dist1.CompareTo(dist2);
    }

    public void TakeDamage(float amount)
    {


        float choice = Random.Range(0f, 3f);

        if (choice > 0 && choice<1)
        {
            hit1Sound.Play();
        }
        else if (choice > 1 && choice<2)
        {
            hit2Sound.Play();
        }
        else
        {
            hit3Sound.Play();
        }



        if (amount>0)
        {
            PopUpController popup = player.GetComponent<PopUpController>();
            popup.DamageDealt((int)amount, Vector3.Distance(player.transform.position, transform.position));
        }
      
        health -= amount;
        Debug.Log(amount);

        if (health<=0)
        {
            Instantiate(enemyDeathExplosion, this.transform.position, Quaternion.identity);
            Instantiate(deathSoundController, this.transform.position, Quaternion.identity);
            GameController.instance.kills++;
            if (tag == "FlyingEnemy1")
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            Vector3 randomAdd = new Vector3(Random.Range(0.1f,2f),0, Random.Range(0.1f, 2f));
            Instantiate(cog, transform.position + randomAdd, Quaternion.identity);

            //if (gameObject.tag=="BasicInfantry")
            //{
            randomAdd = new Vector3(Random.Range(0.1f, 2f), 0, Random.Range(0.1f, 2f));

            //}


            int spawnChip = Random.Range(1,10);// 1,10
            if (spawnChip==2)
            {
                Instantiate(chip1, transform.position + randomAdd, Quaternion.identity);
            }
            else if (spawnChip==3)
            {
                Instantiate(chip2, transform.position + randomAdd, Quaternion.identity);
            }
            



        }
    }

    public void slowSpeed()
    {
        enemy.speed = enemy.speed / 100;
        Invoke("FixSpeed", 5);

    }

    public void FixSpeed()
    {
        enemy.speed = enemy.speed * 100; 
    }

    public void Attack()
    {
        
    }


}
