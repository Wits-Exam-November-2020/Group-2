using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPowerUp : MonoBehaviour
{
    private GameObject player;
    public GameObject passivePickupSoundPrefab;
    private PowerUpUIManager powerupDisplay;

    private void Start()
    {
        powerupDisplay = GameObject.Find("Canvas/HUD/ChipPowerUps").GetComponent<PowerUpUIManager>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");

        if (other.tag == "Player")
        {
            player = GameObject.Find("Player(Clone)");
            StartCoroutine(Move());
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

        }

    }

    IEnumerator Move()
    {

        float timeSinceStart = 0;
        float waitTime = 3.5f;



        while (timeSinceStart < waitTime)
        {
            timeSinceStart += Time.deltaTime;


            transform.position = Vector3.Lerp(transform.position, player.transform.position, 4f * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.transform.position) < 1.7f)
            {
                break;
            }

            yield return null;
        }
        
        Instantiate(passivePickupSoundPrefab, this.transform.position, Quaternion.identity);
        transform.position = player.transform.position;
        ApplyPowerUp();
        Destroy(this.gameObject);


        yield return null;


    }

    private void ApplyPowerUp()
    {
        int space = 8;
        switch (tag)
        {
            case "AmmoPrice":

                if (player.GetComponent<Player>().costOfAmmo - 10 >= 0)
                {
                    player.GetComponent<Player>().costOfAmmo -= 10;
                }
                else
                {
                    player.GetComponent<Player>().costOfAmmo =0 ;
                }
                
                powerupDisplay.findSpace(2);
               // Debug.Log("FOUND SPACE: " + space);

                break;
            case "HealthRegen":
                powerupDisplay.playHealth();
                player.GetComponent<Player>().current_health = player.GetComponent<Player>().max_health;

                break;
            case "IncreaseDamage":
                player.GetComponent<Weapon>().dmgModifier += 10;
                powerupDisplay.findSpace(1);
               // Debug.Log("FOUND SPACE: " + space);

                break;
            case "JetPackRecovery":
                player.GetComponent<Player>().jetRecovery += 0.1f;
                powerupDisplay.findSpace(4);
                //Debug.Log("FOUND SPACE: " + space);
                break;
            case "Shield":
                powerupDisplay.playShield();
                player.GetComponent<Player>().current_shield += 33;
                break;
            case "SpeedIncrease":
                powerupDisplay.findSpace(3);
               // Debug.Log("FOUND SPACE: " + space);
                break;
        }
       
    }
}
