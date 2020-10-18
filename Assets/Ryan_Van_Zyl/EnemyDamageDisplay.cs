using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class EnemyDamageDisplay : MonoBehaviour
{
    public GameObject damageEffect1;
    private EnemyController enemControl;


    private void Awake()
    {
        damageEffect1.SetActive(false);
      
    }

    private void Start()
    {
        enemControl = gameObject.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemControl.health <= (enemControl.maxHealth / 2))
        {
            damageEffect1.SetActive(true);
        }
    }


   

}
