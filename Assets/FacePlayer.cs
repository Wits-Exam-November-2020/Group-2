using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    private GameObject player;
    private float damping= 0.1f;

    private void Awake()
    {
    
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    player = GameObject.Find("Player(Clone)");
    //   // transform.Rotate(0, 180, 0);
    //}

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player(Clone)");
        Vector3 lookPos = player.transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation, Time.deltaTime * damping);
    }
}
