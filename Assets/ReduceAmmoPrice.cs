using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceAmmoPrice : MonoBehaviour
{
    private GameObject player;
    public GameObject passivePickupSoundPrefab;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(passivePickupSoundPrefab, this.transform.position, Quaternion.identity);

        player = GameObject.Find("Player(Clone)");

        player.GetComponent<Player>().costOfAmmo -= 10;
        Destroy(this.gameObject);
    }
}
