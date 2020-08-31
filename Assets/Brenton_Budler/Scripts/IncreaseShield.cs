using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseShield : MonoBehaviour
{
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");
        player.GetComponent<Player>().current_shield += 33/2;
        Destroy(this.gameObject);
    }
}
