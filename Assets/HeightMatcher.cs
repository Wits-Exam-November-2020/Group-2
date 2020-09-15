using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HeightMatcher : MonoBehaviour
{

    private GameObject player;
    private NavMeshAgent enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player(Clone)");

       // transform.position = new Vector3(transform.position.x, player.transform.position.y+1, transform.position.z);


        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y, transform.position.z), 1f);


    }
}
