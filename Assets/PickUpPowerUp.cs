using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPowerUp : MonoBehaviour
{
    private GameObject player;
    public GameObject passivePickupSoundPrefab;


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
        switch (tag)
        {
            case "AmmoPrice": player.GetComponent<Player>().costOfAmmo -= 10;
                break;
            case "HealthRegen":
                player.GetComponent<Player>().current_health = player.GetComponent<Player>().max_health;
                break;
            case "IncreaseDamage":
                player.GetComponent<Weapon>().dmgModifier += 10;
                break;
            case "JetPackRecovery":
                player.GetComponent<Player>().jetRecovery += 0.1f;
                break;
            case "Shield":
                player.GetComponent<Player>().current_shield += 33;
                break;
            case "SpeedIncrease":
                player.GetComponent<Player>().speed += 50;
                break;
        }
       
    }
}
