using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{

    private GameObject player;


    private void OnCollisionEnter(Collision other)
    {
        player = GameObject.Find("Player(Clone)");
        if (other.gameObject.tag == "Player" && gameObject.tag=="Cog")
        {
            GameController.instance.playerWallet.cogs += 1;
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Player" && gameObject.tag == "Nut")
        {
            GameController.instance.playerWallet.nuts += 1;
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Player" && gameObject.tag == "Bolt")
        {
            GameController.instance.playerWallet.bolts += 1;
            Destroy(this.gameObject);
        }


    }
}
