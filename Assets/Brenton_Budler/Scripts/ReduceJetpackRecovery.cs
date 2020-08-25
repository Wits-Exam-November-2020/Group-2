using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceJetpackRecovery : MonoBehaviour
{
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");
        Debug.Log(player.GetComponent<Player>().jetRecovery);
        player.GetComponent<Player>().jetRecovery += (player.GetComponent<Player>().jetRecovery * 0.02f);
        Destroy(this.gameObject);
    }
}
