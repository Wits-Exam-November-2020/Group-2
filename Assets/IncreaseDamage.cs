using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : MonoBehaviour
{
    private GameObject player;
    public GameObject passivePickupSoundPrefab;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Instantiate(passivePickupSoundPrefab, this.transform.position, Quaternion.identity);

            player = GameObject.Find("Player(Clone)");

            player.GetComponent<Weapon>().dmgModifier += 10;
            Destroy(this.gameObject);
        }
      
    }
}
