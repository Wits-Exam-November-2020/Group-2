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
                currentPowerups[0] = null;
            }

            if (currentPowerups[0]!=null && currentPowerups[0].name == "Warrior")
            {
                player.GetComponent<Weapon>().warrior = 2f;
                Invoke("endWarrior", 5);
                currentPowerups[0] = null;
            }

            if (currentPowerups[0] != null && currentPowerups[0].name == "DoubleDamage")
            {
                player.GetComponent<Weapon>().dmgModifier = 2f;
                Invoke("endDoubleDamage", 15);
                currentPowerups[0] = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            player = GameObject.Find("Player(Clone)");

            if (currentPowerups[1] != null && currentPowerups[1].name == "Invincible")
            {
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
                player.GetComponent<Weapon>().dmgModifier = 2f;
                Invoke("endDoubleDamage", 15);
                currentPowerups[1] = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            player = GameObject.Find("Player(Clone)");

            if (currentPowerups[2] != null && currentPowerups[2].name == "Invincible")
            {
                player.GetComponent<Player>().invincible = true;
                Invoke("endInvincible", 8);
                currentPowerups[2] = null;
            }

            if (currentPowerups[2] != null && currentPowerups[2].name == "Warrior")
            {
                player.GetComponent<Weapon>().warrior = 2f;
                Invoke("endWarrior", 5);
                currentPowerups[2] = null;
            }

            if (currentPowerups[2] != null && currentPowerups[2].name == "DoubleDamage")
            {
                player.GetComponent<Weapon>().dmgModifier = 2f;
                Invoke("endDoubleDamage", 15);
                currentPowerups[2] = null;
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

    public void endDoubleDamage()
    {
        player.GetComponent<Weapon>().dmgModifier = 1f;
    }



}
