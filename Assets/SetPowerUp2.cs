using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPowerUp2 : MonoBehaviour
{
    private GameObject player;
    public GameObject dbldmgIcon;
    public GameObject invinIcon;

    public Transform block;
    private float tempScale = 1;


    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player(Clone)");

        if (player.GetComponent<UsablePowerUps>().currentPowerups[1] != null)
        {
            if (player.GetComponent<UsablePowerUps>().currentPowerups[1].name == "Invincible")
            {
                invinIcon.SetActive(true);
                dbldmgIcon.SetActive(false);
            }
            else if (player.GetComponent<UsablePowerUps>().currentPowerups[1].name == "DoubleDamage")
            {
                invinIcon.SetActive(false);
                dbldmgIcon.SetActive(true);
            }
        }
        else
        {
            if (player.GetComponent<UsablePowerUps>().usingDoubleDamage)
            {
                invinIcon.SetActive(false);
            }
            else if (player.GetComponent<UsablePowerUps>().usingInvincible)
            {
                dbldmgIcon.SetActive(false);
            }
            else
            {
                invinIcon.SetActive(false);
                dbldmgIcon.SetActive(false);
            }


        }

        if (player.GetComponent<UsablePowerUps>().usingDoubleDamage)
        {
            tempScale -= 0.065f * Time.deltaTime;
            block.localScale = new Vector3(1, tempScale, 1);
        }
        else if (player.GetComponent<UsablePowerUps>().usingInvincible)
        {
            tempScale -= 0.125f * Time.deltaTime;
            block.localScale = new Vector3(1, tempScale, 1);
        }
        else
        {
            block.localScale = new Vector3(1, 1, 1);
        }

    }
}
