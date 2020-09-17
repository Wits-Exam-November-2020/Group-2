using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject cog;
    public int damage = 100;
    private GameObject player;


    public NavMeshAgent enemy;
    private Transform target;
    public float health;
    public float maxHealth;

    private bool isAttacking = false;

    private Vector3 playerLoc;

    private void Start()
    {
        
        health = maxHealth;
       // enemy = this.GetComponent<NavMeshAgent>();
       
    }


    void Update()
    {
        player = GameObject.Find("Player(Clone)");
        target = GameObject.Find("Player(Clone)").transform; 
        enemy.SetDestination(target.position);



        //transform.Translate(0,1f,0);

        if (gameObject.tag=="FlyingEnemy1")
        {



            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, player.transform.position.y, transform.localPosition.z), 0.5f * Time.deltaTime);



            playerLoc.x = player.transform.position.x;
            playerLoc.z = player.transform.position.z;
            playerLoc.y = enemy.transform.position.y;

            if (Vector3.Distance(enemy.transform.position, playerLoc) <= enemy.stoppingDistance)
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    player.GetComponent<Player>().TakeDamage(damage);
                    Invoke("ResetAttack", 2);


                }
                Debug.Log("Attack");
            }
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position, target.position) <= enemy.stoppingDistance)
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    player.GetComponent<Player>().TakeDamage(damage);
                    Invoke("ResetAttack", 2);


                }
                Debug.Log("Attack");
            }

        }








    }

    public void ResetAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(health);
        if (health<=0)
        {
            GameController.instance.kills++;
            Destroy(gameObject);
            
            
            if (gameObject.tag=="BasicInfantry")
            {
              //  Instantiate(cog, transform.position, transform.rotation);
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
