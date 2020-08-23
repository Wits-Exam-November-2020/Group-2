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
    //public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;

    //Lifetime
        public int maxCollisions;
        public float maxLifeTime;
        public bool explodeOnTouch = true;



    int collisions;
    PhysicMaterial physics_mat;


    //type
    public bool isEMP;
    public bool isGrenade;

    private void Start()
    {
        SetUp();
    }

    private void Update()
    {

        if (isGrenade)
        {
            //When to explode
            if (collisions > maxCollisions) ExplodeGren();

            //Countdown Lifetime
            maxLifeTime -= Time.deltaTime;
            if (maxLifeTime <= 0) ExplodeGren();
        }else if (isEMP)
        {
            //When to explode
            if (collisions > maxCollisions) ExplodeEMP();

            //Countdown Lifetime
            maxLifeTime -= Time.deltaTime;
            if (maxLifeTime <= 0) ExplodeEMP();
        }

    }

    private void ExplodeGren()
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



    private void ExplodeEMP()
    {
        //Instatiate explosion
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);


        //Check for enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().slowSpeed();
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
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch)
        {
            if (isGrenade)
            {
                 ExplodeGren();
            }
            else if (isEMP)
            {
                ExplodeEMP();
            }


        }
    }

    private void SetUp()
    {
        //Create a new physic material 
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.dynamicFriction = 100;
        physics_mat.staticFriction = 100;
       physics_mat.frictionCombine = PhysicMaterialCombine.Maximum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        //Assign Material 
        GetComponent<SphereCollider>().material = physics_mat;


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }



}
