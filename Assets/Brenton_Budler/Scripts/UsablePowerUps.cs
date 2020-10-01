using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsablePowerUps : MonoBehaviour
{

    public Powerup[] currentPowerups = new Powerup[2];
    private GameObject player;

    
    public bool usingDoubleDamage;
    public bool usingInvincible; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player = GameObject.Find("Player(Clone)");

            if (currentPowerups[0]!=null && currentPowerups[0].name=="Invincible")
            {
                usingInvincible = true;
                player.GetComponent<Player>().invincible = true;
                Invoke("endInvincible", 8);
                currentPowerups[0] = null;
            }

            if (currentPowerups[0]!=null && currentPowerups[0].name == "Warrior")
            {
                player.GetComponent<Weapon>().warrior = 2f;
                Invoke("endWarrior", 5);
                currentPowerups[0] = null;
            }

            //if (currentPowerups[0] != null && currentPowerups[0].name == "DoubleDamage")
            //{
            //    usingDoubleDamage = true;
            //    player.GetComponent<Weapon>().dmgModifier = 2;
            //    Invoke("endDoubleDamage", 15);
            //    currentPowerups[0] = null;
            //}
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player = GameObject.Find("Player(Clone)");

            if (currentPowerups[1] != null && currentPowerups[1].name == "Invincible")
            {
                usingInvincible = true;
                player.GetComponent<Player>().invincible = true;
                Invoke("endInvincible", 8);
                currentPowerups[1] = null;
            }

            if (currentPowerups[1] != null && currentPowerups[1].name == "Warrior")
            {
                player.GetComponent<Weapon>().warrior = 2f;
                Invoke("endWarrior", 5);
                currentPowerups[1] = null;
            }

            if (currentPowerups[1] != null && currentPowerups[1].name == "DoubleDamage")
            {
                usingDoubleDamage = true; 
                player.GetComponent<Weapon>().dmgModifier = 2;
                Invoke("endDoubleDamage", 15);
                currentPowerups[1] = null;
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
        }
       
        
        return 4;
    }

    public void endInvincible()
    {
        usingInvincible = false;
        player.GetComponent<Player>().invincible = false;
    }

    public void endWarrior()
    {
        player.GetComponent<Weapon>().warrior = 1f;
    }

    public void endDoubleDamage()
    {
        usingDoubleDamage = false;
        player.GetComponent<Weapon>().dmgModifier = 1;
    }



}
