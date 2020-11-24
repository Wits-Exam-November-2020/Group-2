using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject muzzleFlash;
    private GameObject currentMuzzle;

    public GameObject projectilePrefab; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ShootProjectile();
        }
    }


    public void ShootProjectile()
    {
        currentMuzzle = Instantiate(muzzleFlash, transform.position, transform.rotation) as GameObject;
        currentMuzzle.transform.parent = transform;
        GameObject currentBullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        currentBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
        Debug.Log(this.GetComponent<Rigidbody>().velocity.y);
    }
}
