using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseShield : MonoBehaviour
{
    private GameObject player;
    public GameObject passivePickupSoundPrefab;


    private void OnTriggerEnter(Collider other)
    {
        Instantiate(passivePickupSoundPrefab, this.transform.position, Quaternion.identity);

        player = GameObject.Find("Player(Clone)");
        player.GetComponent<Player>().current_shield += 33/2;
        Destroy(this.gameObject);
    }
}
