using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealth : MonoBehaviour
{
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");

        player.GetComponent<Player>().current_health = player.GetComponent<Player>().max_health;
        Destroy(this.gameObject);
    }
}
