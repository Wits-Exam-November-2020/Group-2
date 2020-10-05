using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : MonoBehaviour
{
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");

        player.GetComponent<Weapon>().dmgModifier += 10;
        Destroy(this.gameObject);
    }
}
