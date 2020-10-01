using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject cog;
    public int damage = 100;
    public float rotationSpeed = 0.5f;
    private float moveSpeed = 2f;
    public float startSpeed = 3f;
    public float raycastOffset = 1;
    public float rayDistance;
    public float turnSpeed;
    private GameObject player;

    public NavMeshAgent enemy;
    private Transform target;
    public float health;
    public float maxHealth;

    private bool isAttacking = false;

    private void Start()
    { 
        health = maxHealth;
        moveSpeed = startSpeed;
       
            
        
    }


    void Update()
    {

        player = GameObject.Find("Player(Clone)");
        target = GameObject.Find("Player(Clone)").transform;
      

        ////point();
        if (gameObject.tag == "FlyingEnemy1")
        {
            rays();
            if (Vector3.Distance(transform.position, player.transform.position) > 5)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
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
                    Invoke("ResetAttack", 2);


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
        //if (Physics.Raycast(down, transform.forward, out hit, rayDistance))
        //{
        //  // turn -= Vector3.up;
        //}
        //else if (Physics.Raycast(up, transform.forward, out hit, rayDistance))
        //{
        //   // turn += Vector3.down;
        //}
       
        if (turn != Vector3.zero )
        {
            if (hit.collider!=null)
            {
                if (hit.collider.tag == "Building")
                {
                    moveSpeed = hit.distance-0.5f;
                    transform.Rotate(turn * turnSpeed * Time.deltaTime);
                }

                if (hit.collider.tag=="FlyingEnemy1")
                {
                    //moveSpeed = hit.distance - 0.5f;
                   transform.Rotate(turn * turnSpeed * Time.deltaTime);
                 transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 0.5f*Time.deltaTime);
                   
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
