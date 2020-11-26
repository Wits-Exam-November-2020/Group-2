using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageEffects : MonoBehaviour
{

    private BossController bController;
    public GameObject sEffect1, sEffect2, sEffect3, sEffect4;
    public GameObject explosionEffect;
    public GameObject partsExplode;
    public Transform spawnEffects;

    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        bController = this.GetComponent<BossController>();
        maxHealth = bController.health;

        sEffect1.SetActive(false);
        sEffect2.SetActive(false);
        sEffect3.SetActive(false);
        sEffect4.SetActive(false);

     

    }

    // Update is called once per frame
    void Update()
    {
        if (bController.health <= (maxHealth/1.25))
        {
            sEffect1.SetActive(true);
        }
        if (bController.health <= (maxHealth/1.6))
        {
            sEffect2.SetActive(true);
        }
        if (bController.health <= (maxHealth/2.3))
        {
            sEffect3.SetActive(true);
        }
        if (bController.health <= (maxHealth/3.3))
        {
            sEffect4.SetActive(true);
        }

        if (bController.health <= 0)
        {
            Instantiate(partsExplode, spawnEffects.position, Quaternion.identity);
            Instantiate(explosionEffect, spawnEffects.position, Quaternion.identity);
        }

    }
}
