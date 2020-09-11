﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogChoke : MonoBehaviour
{
    private bool isChoking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y>20)
        {
            if (!isChoking)
            {
                isChoking = true;
                choke();
            }
        }
    }

    public void choke()
    {
        this.GetComponent<Player>().TakeDamage(10);
        Invoke("StopChoke", 2);
    }

    public void StopChoke()
    {
        isChoking = false;
    }
}
