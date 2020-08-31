using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsablePowerUps : MonoBehaviour
{

    public Powerup[] currentPowerups = new Powerup[3];
    private GameObject player;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            player = GameObject.Find("Player(Clone)");

            if (currentPowerups[0]!=null && currentPowerups[0].name=="Invincible")
            {
                player.GetComponent<Player>().invincible = true;
                Invoke("endInvincible", 8);
            }

            if (currentPowerups[0]!=null && currentPowerups[0].name == "Warrior")
            {
                player.GetComponent<Weapon>().warrior = 2f;
                Invoke("endWarrior", 5);
            }
        }
    }

    public int checkSpace()
    {
        if (currentPowerups[0] == null)
        {
            return 0;
        }
        else if(currentPowerups[1] == null)
        {
            return 1;
        }else if (currentPowerups[2] == null)
        {
            return 2;
        }
       
        
        return 4;
    }

    public void endInvincible()
    {
        player.GetComponent<Player>().invincible = false;
    }

    public void endWarrior()
    {
        player.GetComponent<Weapon>().warrior = 1f;
    }





}
