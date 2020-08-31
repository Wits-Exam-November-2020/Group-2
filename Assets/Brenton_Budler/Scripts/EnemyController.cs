﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject cog; 
 

    private NavMeshAgent enemy;
    private Transform target;
    private float health;
    private float maxHealth = 5;

    private void Start()
    {
        health = maxHealth;
        enemy = this.GetComponent<NavMeshAgent>();
       
    }


    void Update()
    {
        target = GameObject.Find("Player(Clone)").transform; 
       enemy.SetDestination(target.position);

        if (Vector3.Distance(enemy.transform.position,target.position) <= enemy.stoppingDistance)
        {
            Debug.Log("Attack");
        }

        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(health);
        if (health<=0)
        {
            Destroy(gameObject);
            if (gameObject.tag=="BasicInfantry")
            {
                Instantiate(cog, transform.position, transform.rotation);
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
}
