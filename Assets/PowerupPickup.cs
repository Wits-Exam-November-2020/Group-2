using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    private GameObject player;
    public Powerup powerup;


    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");
        int ind = player.GetComponent<UsablePowerUps>().checkSpace();
        if (ind != 4)
        {
            player.GetComponent<UsablePowerUps>().currentPowerups[ind] = powerup;
        }


    }
}
