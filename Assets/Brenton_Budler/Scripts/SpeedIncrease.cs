using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : MonoBehaviour
{

    private GameObject player;
    public GameObject passivePickupSoundPrefab;


    private void OnTriggerEnter(Collider other)
    {
        Instantiate(passivePickupSoundPrefab, this.transform.position, Quaternion.identity);
        player = GameObject.Find("Player(Clone)");

        player.GetComponent<Player>().speed += 50;
        Destroy(this.gameObject);
    }
}
