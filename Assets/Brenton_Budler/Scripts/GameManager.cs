using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player_prefab;
    public Transform[] spawn_points;
    public Wallet playerWallet;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
      // Spawn();
    }

    public void Spawn()
    {
        Transform t_spawn = spawn_points[Random.Range(0,spawn_points.Length)];
        Instantiate(player_prefab, t_spawn.position, t_spawn.rotation);
    }


}
