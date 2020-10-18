using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    public AudioSource wind;
    private GameObject player;
    public float volumeChangeFactor;

    private void Start()
    {
        wind.volume = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        

        if (player.transform.position.y >= 30f)
        {

            if (wind.volume < 0.11f)
            {
                wind.volume += volumeChangeFactor * Time.deltaTime;
                Debug.Log("increase volume");
            }

        }
        else if (player.transform.position.y < 34f)
        {
            if (wind.volume > 0f)
            {
                Debug.Log("Decrease volume");
                wind.volume -= volumeChangeFactor * Time.deltaTime;
            }
        }
    }


  



}
