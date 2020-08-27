using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpActions : MonoBehaviour
{
    private GameObject player;


    public void invincibleStartAction()
    {
        player = GameObject.Find("Player(Clone)");
        player.GetComponent<Player>().invincible = true;
      
    }

    public void invincibleEndAction()
    {
        player.GetComponent<Player>().invincible = false;
    } 
}
