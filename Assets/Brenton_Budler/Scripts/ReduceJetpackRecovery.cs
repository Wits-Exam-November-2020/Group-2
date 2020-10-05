﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceJetpackRecovery : MonoBehaviour
{
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");
        player.GetComponent<Player>().jetRecovery += 0.1f ;
        Destroy(this.gameObject);
    }
}
