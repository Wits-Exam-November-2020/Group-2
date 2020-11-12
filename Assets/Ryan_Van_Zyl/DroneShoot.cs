using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShoot : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 15f;
    public int damage;

    [Header("Required Components")]
    public Transform shootPoint;
    public GameObject projectile;


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
            GameObject bulletGo = (GameObject)Instantiate(projectile, shootPoint.position, Quaternion.identity);
            BulletMovement bulletMov = bulletGo.GetComponent<BulletMovement>();
            if (bulletMov != null)
            {
                bulletMov.Seek(target, damage);
            }
        }

      

        
    }
}
