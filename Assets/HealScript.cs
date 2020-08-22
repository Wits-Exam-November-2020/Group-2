using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour
{
    public float healRange;
    public LayerMask whatisPlayer;

    // Update is called once per frame
    void Update()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, healRange, whatisPlayer);
        if (player!=null)
        {
            for (int i = 0; i < player.Length; i++)
            {

                player[i].GetComponent<Player>().Heal();
            }
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRange);
    }

    } 


