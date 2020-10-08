using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSoundEnemyController : MonoBehaviour
{
    public AudioSource deathSound1;

    // Start is called before the first frame update
    void Start()
    {
        int randomNum = Random.Range(0, 1);
        if (randomNum == 1)
        {
            deathSound1.Play();
        }
    }

    
}
