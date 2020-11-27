using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Transform target;
    private int damage;

    public float speed = 70f;
    private Player player;
    private Vector3 dir;

    public void Seek(Transform _target, int _damage)
    {
        target = _target;
        damage = _damage;
    }

    private void Start()
    {
        
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dir = target.position - transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player(Clone)").GetComponent<Player>();

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        float distanceThisFrame = speed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "FlyingEnemy1")
        {
            Destroy(gameObject);

        }

        if (other.tag == "Player")
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }

       
    }


  
}
