using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DamageSoundController : MonoBehaviour
{
    public static bool playDamageSound;
    public AudioSource grunt1, grunt2, grunt3;

    private void Awake()
    {
        playDamageSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playDamageSound == true)
        {
            playDamageSound = false;
            int randomNum = Random.Range(0, 4);

            if(randomNum == 1)
            {
          
                grunt1.Play();
            }
            else if (randomNum == 2)
            {
                grunt2.Play();
            }
            else
            {
                grunt3.Play();
            }
        }
    }

   
}
