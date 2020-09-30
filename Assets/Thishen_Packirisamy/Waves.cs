using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
}
[System.Serializable]
public class Enemy
{
    public int amount;
    public int EnemyPrefabIndex;
}


