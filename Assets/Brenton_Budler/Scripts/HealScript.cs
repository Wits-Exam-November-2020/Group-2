using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour
{
    public float healRange;
    public LayerMask whatisPlayer;
    public LayerMask whatisHealer;

    // Update is called once per frame
    void Update()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, healRange, whatisPlayer);

        if (player.Length>0) {
            for (int i = 0; i < player.Length; i++)
            {
                player[i].GetComponentInParent<Player>().Heal();

            }
        }

        Collider[] otherHealers = Physics.OverlapSphere(transform.position, healRange, whatisHealer);

        if (otherHealers.Length > 0)
        {
            for (int i = 0; i < otherHealers.Length-1; i++)
            {
                Destroy(otherHealers[i]);

            }
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRange);
    }

    } 


