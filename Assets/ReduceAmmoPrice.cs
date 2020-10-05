using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceAmmoPrice : MonoBehaviour
{
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");

        player.GetComponent<Player>().costOfAmmo -= 10;
        Destroy(this.gameObject);
    }
}
