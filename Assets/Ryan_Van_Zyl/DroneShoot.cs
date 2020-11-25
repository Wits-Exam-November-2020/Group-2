using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShoot : MonoBehaviour
{

    private Transform target;
    private GameObject currentMuzzle;
    private Vector3 knockBackDirection;

    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 15f;
    public int damage;
    public float knockBackForce;
    

    [Header("Required Components")]
    public Transform shootPoint;
    public GameObject projectile;
    public GameObject muzzleFlash;
    public AudioSource shootSound;
    public Rigidbody rb;
    





    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
                
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        if ((Vector3.Distance(this.transform.position, target.position)) <= range)
        {
            GameObject bulletGo = (GameObject)Instantiate(projectile, shootPoint.position, shootPoint.rotation);
            BulletMovement bulletMov = bulletGo.GetComponent<BulletMovement>();

            if (bulletMov != null)
            {
                bulletMov.Seek(target, damage);
                currentMuzzle = Instantiate(muzzleFlash, shootPoint.position, shootPoint.rotation) as GameObject;
                currentMuzzle.transform.parent = shootPoint;
                KnockBack();
                shootSound.Play();               
            }
        }       
    }

    private void KnockBack()
    {
        knockBackDirection = this.transform.position - target.transform.position;
        rb.AddForce(knockBackDirection.normalized * knockBackForce);
        Invoke("ResetKnockBack", 0.25f);
        
    }
    private void ResetKnockBack()
    {
        rb.velocity = Vector3.zero;
    }
}
