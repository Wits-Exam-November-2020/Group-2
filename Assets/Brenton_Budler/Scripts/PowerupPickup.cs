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
        if (other.tag=="Player" && ind != 4)
        {
            player.GetComponent<UsablePowerUps>().currentPowerups[ind] = powerup;
            Destroy(this.gameObject);
        }


    }
}
