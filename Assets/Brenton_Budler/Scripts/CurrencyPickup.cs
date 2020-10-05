using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{

    private GameObject player;


    private void OnCollisionEnter(Collision other)
    {
      



    }


    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");
        if (other.gameObject.tag == "Player" && gameObject.tag == "Cog")
        {
            player.GetComponent<Player>().wallet += 25;
            Destroy(this.gameObject);
        }
    }
}
