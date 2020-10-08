using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{

    private GameObject player;
    public GameObject currencyPickupSoundprefab;


    private void OnCollisionEnter(Collision other)
    {
      



    }


    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");
        if (other.gameObject.tag == "Player" && gameObject.tag == "Cog")
        {
            Instantiate(currencyPickupSoundprefab, this.transform.position, Quaternion.identity);
            player.GetComponent<Player>().wallet += 25;
            Destroy(this.gameObject);
        }
    }
}
