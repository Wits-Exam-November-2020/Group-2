﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public List<GameObject> weaponPrefabs;
    private List<GameObject> currentPrefabs;
    private GameObject[] spawnPoints;
    void Start()
    {
        currentPrefabs = new List<GameObject>(weaponPrefabs); 
        
        spawnPoints = GameObject.FindGameObjectsWithTag("WeaponSpawnPoint");
        foreach (GameObject spawnPoint in spawnPoints)
        {
           
            int index = Random.Range(0, currentPrefabs.Count);
            
            GameObject weapon = currentPrefabs[index];
            
            Instantiate(weapon, spawnPoint.transform.position, spawnPoint.transform.rotation);
            currentPrefabs.RemoveAt(index);


            if (currentPrefabs.Count <= 0)
            {
               
                currentPrefabs = new List<GameObject>(weaponPrefabs);
             
            }

        }
    }

    public void newGun(string tag, Transform pos)
    {
        switch (tag)
        {
            case "Pistol": Instantiate(weaponPrefabs[0], pos);

                break;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
