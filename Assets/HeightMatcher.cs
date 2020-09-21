using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HeightMatcher : MonoBehaviour
{

    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player(Clone)");

        // transform.position = new Vector3(transform.position.x, player.transform.position.y+1, transform.position.z);


        // transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y+1, transform.position.z), 0.5f * Time.deltaTime);

        //transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, player.transform.position.y, transform.localPosition.z), 0.5f * Time.deltaTime);
        //if (player.transform.position.y>20) {
        //    transform.position = new Vector3(transform.position.x, player.transform.position.y + 1, transform.position.z);
        //}
        //else
        //{
            
        //}
    }
}
