using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceJetpackRecovery : MonoBehaviour
{
    private GameObject player;
    public GameObject passivePickupSoundPrefab;


    private void OnTriggerEnter(Collider other)
    {
        Instantiate(passivePickupSoundPrefab, this.transform.position, Quaternion.identity);

        player = GameObject.Find("Player(Clone)");
        player.GetComponent<Player>().jetRecovery += 0.1f ;
        Destroy(this.gameObject);
    }
}
