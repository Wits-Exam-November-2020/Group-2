using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gun", menuName ="Gun")]
public class Gun : ScriptableObject
{
    public string gunName;
    public int damage;

    public int ammo;
    public int clipSize;
    public float reloadTime;

    public float firerate;
    public float bloom;
    public float recoil;
    public float kickback;
    public float aimSpeed;

    public int burst; //0 semi 1 auto //2-> burst fire

    public bool isProjectile;
    public GameObject projectilePrefab;
    public int projectileForce;

    //shotgun
    public int pellets;
    public bool recovery;

    //SOUND
    public AudioClip gunshotSound;
    public float pitchRandomization;

    public GameObject prefab;

    private int stash; //current ammo
    private int clip; //current clip

    public GameObject muzzleFlash;

    public void Initialize()
    {
        stash = ammo;
        clip = clipSize;

    }

    public bool FireBullet()
    {
        if (clip > 0)
        {
            clip -= 1;
            return true;
        }
        else return false;
    }

    public void  Reload()
    {
        stash += clip;
        clip = Mathf.Min(clipSize, stash);
        stash -= clip;
    }

    public int GetStash()
    {
        return stash;
    }

    public int GetClip()
    {
        return clip;
    }
}
