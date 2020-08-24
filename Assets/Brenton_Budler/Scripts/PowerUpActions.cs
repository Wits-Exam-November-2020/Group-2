using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpActions : MonoBehaviour
{
    [SerializeField]
    public Player player;


    public void HighSpeedStartAction()
    {
        Debug.Log("FASTER");
        player.speed*= 2;
      
    }

    public void HighSpeedEndAction()
    {
        player.speed = player.defaultSpeed;
    } 
}
