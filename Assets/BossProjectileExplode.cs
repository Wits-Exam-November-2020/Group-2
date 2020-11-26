﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileExplode : MonoBehaviour
{
    private int explosionDamage;

    public int minDamage;
    public int maxDamage;

    public float explosionRange;
    public LayerMask whatIsPlayer;
    public AudioSource explodeSound;
    private Renderer rend;
    private Collider col;


    //public GameObject child;
    public GameObject explosionEffect;



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        Exploding();
    }


    private void Exploding()
    {


        explosionDamage = Random.Range(minDamage, maxDamage);
        //Instatiate explosion
        //if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        //Sound
        col = GetComponent<Collider>();
        col.enabled = false;
        rend = GetComponent<Renderer>();
        rend.enabled = false;

        //  explodeSound.Play();

        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        //Check for enemies
        Collider[] player = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
        for (int i = 0; i < player.Length; i++)
        {
            player[i].GetComponentInParent<Player>().TakeDamage(explosionDamage);
            //PopUpController popup = new PopUpController();
        }

        //Add a little delay 
        Invoke("Delay", 0.01f);
    }

    private void Delay()
    {
        Destroy(this.gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}