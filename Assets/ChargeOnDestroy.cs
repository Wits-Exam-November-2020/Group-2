using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeOnDestroy : MonoBehaviour
{
    private GameObject player;

    private void OnDestroy()
    {
        player = GameObject.Find("Player(Clone)");

        player.GetComponent<Player>().wallet -= 100;
        
    }
}
