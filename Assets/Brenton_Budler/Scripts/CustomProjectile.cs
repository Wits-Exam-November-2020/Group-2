using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomProjectile : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;


    //Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;

    //Lifetime
        public int maxCollisions;
        public float maxLifeTime;
        public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    private void Start()
    {
        SetUp();
    }

    private void Update()
    {
        //When to explode
        if (collisions > maxCollisions) Explode();

        //Countdown Lifetime
        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0) Explode();
    }

    private void Explode()
    {
        //Instatiate explosion
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);


        //Check for enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i =0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().TakeDamage(explosionDamage);
        }

        //Add a little delay 
        Invoke("Delay", 0.05f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Countup number of collisions
        collisions++;

        //Explode if bullet hits an enemy and explodeonTOuch is activated
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode();
    }

    private void SetUp()
    {
        //Create a new physic material 
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        //Assign Material 
        GetComponent<SphereCollider>().material = physics_mat;

        //Set gravity
        rb.useGravity = useGravity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }



}
