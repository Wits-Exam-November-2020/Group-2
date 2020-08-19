using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    public int explosionDamage;
    public float explosionRange;
    public LayerMask whatIsEnemies;


    private void OnCollisionEnter(Collision collision)
    {
        Exploding();
    }


    private void Exploding()
    {
        //Instatiate explosion
        //if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);


        //Check for enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().TakeDamage(explosionDamage);
        }

        //Add a little delay 
        Invoke("Delay", 0.01f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
