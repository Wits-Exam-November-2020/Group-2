using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealth : MonoBehaviour
{
    private GameObject player;
    public GameObject passivePickupSoundPrefab;


    private void OnTriggerEnter(Collider other)
    {
        Instantiate(passivePickupSoundPrefab, this.transform.position, Quaternion.identity);

        player = GameObject.Find("Player(Clone)");

        player.GetComponent<Player>().current_health = player.GetComponent<Player>().max_health;
        Destroy(this.gameObject);
    }
}
