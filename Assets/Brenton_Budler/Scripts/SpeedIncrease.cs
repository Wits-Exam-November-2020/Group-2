using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : MonoBehaviour
{

    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");

        player.GetComponent<Player>().speed += (player.GetComponent<Player>().speed * 0.01f);
        Destroy(this.gameObject);
    }
}
