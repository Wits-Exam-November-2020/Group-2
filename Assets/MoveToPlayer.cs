using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    private GameObject player;

    

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player(Clone)");
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 5f * Time.deltaTime);



    }
}
