using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{

    private GameObject player;
    public GameObject currencyPickupSoundprefab;
    


    private void OnCollisionEnter(Collision other)
    {
      



    }


    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player(Clone)");
        if (other.gameObject.tag == "Player" && gameObject.tag == "Cog")
        {
            StartCoroutine(Move());
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic=true;
        }
    }

    IEnumerator Move()
    {
       
        float timeSinceStart = 0;
        float waitTime = 3.5f;


   
        while (timeSinceStart < waitTime)
        {
            timeSinceStart += Time.deltaTime;

            
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 3.5f * Time.deltaTime);
            if (Vector3.Distance(transform.position,player.transform.position)<1.7f)
            {
                break;
            }
            
            yield return null;
        }
        player.GetComponent<Player>().wallet += 25;
        Instantiate(currencyPickupSoundprefab, this.transform.position, Quaternion.identity);
        transform.position = player.transform.position;
        Destroy(this.gameObject);


        yield return null;


    }
}
