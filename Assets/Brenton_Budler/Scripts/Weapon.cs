﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    #region Varaibles 
    public Gun[] loadout;
    public Transform weaponParent;
    public GameObject bulletHolePrefab;
    public LayerMask canBeShot;

    private float currentCooldown;
    public int currentIndex;
    public GameObject currentWeapon;

    public float warrior;
    public int currentAmmo;
    public AudioSource ammoEmptySound;
    public int maxAmmo ;

    private Transform ui_ammobar;

    //SOUND
    public AudioSource sfx;
    // public AudioClip hitmarkerSound;

    private Image hitmarkerImage;
    private float hitmarkerWait;

    private bool isReloading;
    public bool isAiming = false;


    public int dmgModifier;
    private Transform muzzleFashPoint;
    private GameObject currentMuzzleFlash;

    public PopUpController popups;

    private GameObject infiniteAmmmo;
    private GameObject ammoBackGround;
    private GameObject ammoBar;

    public float aimFOVModifier;

    public GameObject enemySparksImpact;
    public GameObject environmentalImpact;

    //bullet trail stuff
    public LineRenderer bulletTrail;

    #endregion





    #region Built-in Functions

    private void Start()
    {
        maxAmmo = 100;
        currentAmmo = maxAmmo;

        dmgModifier = 0;

        warrior = 1;
        foreach (Gun a in loadout)
        {
            a.Initialize();
        }

        Equip(0);

        hitmarkerImage = GameObject.Find("HUD/HitMarker/Image").GetComponent<Image>();
        hitmarkerImage.color = new Color(0, 1, 0, 0);

        infiniteAmmmo = GameObject.Find("HUD/AmmoBar/InfiniteAmmo");
        ammoBackGround = GameObject.Find("HUD/AmmoBar/BackGround");
        ammoBar = GameObject.Find("HUD/AmmoBar/Bar");
    }

    void Update()
    {
    


        //Ammo 
        if (currentAmmo<=0)
        {
            currentAmmo = 0;
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    currentAmmo = maxAmmo;
        //}

        if (currentWeapon!=null && !PauseMenu.isPaused)
        {
            Aim(Input.GetMouseButton(1));
            if (loadout[currentIndex].burst != 1)
            {
                if (loadout[currentIndex].burst!=1) {
                    if (Input.GetMouseButtonDown(0) && currentCooldown <= 0 && isReloading == false)
                    {
                        if (loadout[currentIndex].name == "Pistol")
                        {
                            Shoot();
                        }
                        else
                        {
                            if (currentAmmo > 0)
                            {

                                Shoot();
                            }
                            else
                            { //no ammo);
                                ammoEmptySound.Play();
                            }
                        }
                    }
                }
            }
            else
            {
                if (Input.GetMouseButton(0) && currentCooldown <= 0 && isReloading==false)
                {
                    if (loadout[currentIndex].name=="Pistol")
                    {
                        Shoot();
                    }
                    else
                    {
                        if (currentAmmo > 0)
                        {

                            Shoot();
                        }
                        else
                        { //no ammo);

                        }
                    }
                   
                }
            }


            //weapon position elasticity 
            currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime*4f);

            //cooldown for weapon
            if (currentCooldown>0){ currentCooldown -= Time.deltaTime; }


        }

        if (hitmarkerWait>0)
        {
            hitmarkerWait -= Time.deltaTime;
        }
        else if (hitmarkerImage.color.a > 0)
        {
            hitmarkerImage.color = Color.Lerp(hitmarkerImage.color, new Color(0, 1, 0, 0), Time.deltaTime*0.5f);
        }


        if (currentWeapon.tag=="Pistol")
        {
            infiniteAmmmo.SetActive(true);
            ammoBackGround.SetActive(false);
            ammoBar.SetActive(false);
        }
        else
        {
            infiniteAmmmo.SetActive(false);
            ammoBackGround.SetActive(true);
            ammoBar.SetActive(true);

        }

        if(isAiming)
        {
          //  this.GetComponent<Player>().normalCam.fieldOfView = Mathf.Lerp(this.GetComponent<Player>().normalCam.fieldOfView, this.GetComponent<Player>().baseFOV * loadout[currentIndex].fovModifier , Time.deltaTime * loadout[currentIndex].aimSpeed);
            //this.GetComponent<Player>().normalCam.fieldOfView = this.GetComponent<Player>().baseFOV * aimFOVModifier;

        }

    }

    #endregion

    #region Own Methods

    IEnumerator Reload(float p_wait)
    {
        isReloading = true;
        currentWeapon.SetActive(false);

        yield return new WaitForSeconds(p_wait);

       // loadout[currentIndex].Reload();
        currentWeapon.SetActive(true);
        isReloading = false;
    }


    public void Equip(int p_ind)
    {

        if (currentWeapon != null){

            if (isReloading)
            {
                StopCoroutine("Reload");
            }
            Destroy(currentWeapon);

        }

        currentIndex = p_ind;

        GameObject t_newEquipment = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        t_newEquipment.transform.localPosition = Vector3.zero;
        t_newEquipment.transform.localEulerAngles = Vector3.zero;

        currentWeapon = t_newEquipment;
    }

    public void Drop()
    {

        //GameObject droppedWeapon = Instantiate(currentWeapon, transform.position, Quaternion.identity) as GameObject;
        //Destroy(currentWeapon);
        //droppedWeapon.transform.localPosition = Vector3.Lerp(droppedWeapon.transform.localPosition, weaponParent.forward*100f, Time.deltaTime * 0.25f);

    }

    void Aim(bool p_isAiming)
    {
        isAiming = p_isAiming;
        Transform t_anchor = currentWeapon.transform.Find("Anchor");
        Transform t_state_ads = currentWeapon.transform.Find("States/ADS");
        Transform t_state_hip = currentWeapon.transform.Find("States/Hip");

        if (p_isAiming)
        {
            //Aim
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
        }
        else
        {
            //Aim
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
        }

        

    }

    void Shoot()
    {

        //Generate random amount of damage
        //int tempDamage = Random.Range(loadout[currentIndex].minDamage, loadout[currentIndex].maxDamage);
        


        if (isAiming)
        {
            muzzleFashPoint = currentWeapon.transform.Find("States/ADS/MuzzleFlashPointADS");
            currentMuzzleFlash = Instantiate(loadout[currentIndex].muzzleFlash, muzzleFashPoint.position, muzzleFashPoint.rotation) as GameObject;
            
            currentMuzzleFlash.transform.parent = muzzleFashPoint;
        }
        else
        {
            muzzleFashPoint = currentWeapon.transform.Find("States/Hip/MuzzleFlashPointHIP");
            currentMuzzleFlash = Instantiate(loadout[currentIndex].muzzleFlash, muzzleFashPoint.position, muzzleFashPoint.rotation) as GameObject;
            currentMuzzleFlash.transform.parent = muzzleFashPoint;
        }


       

        Transform t_spawn = transform.Find("Cameras/Player Camera");


        //Ammo
        if (currentAmmo - loadout[currentIndex].bulletValue>=0)
        {
            currentAmmo -= loadout[currentIndex].bulletValue;
        }
        else
        {
            currentAmmo = 0;
        }
        


        //cooldown
        currentCooldown = loadout[currentIndex].firerate / warrior;

        for (int i = 0; i < Mathf.Max(1,loadout[currentIndex].pellets); i++)
        {
            int tempDamage = Random.Range(loadout[currentIndex].minDamage, loadout[currentIndex].maxDamage);

            //bloom (ACUURACY)
            Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
            t_bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.up;
            t_bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.right;
            t_bloom -= t_spawn.position;
            t_bloom.Normalize();

            //raycast
            RaycastHit t_hit = new RaycastHit();
            if (Physics.Raycast(t_spawn.position, t_bloom, out t_hit, 1000f, canBeShot))
            {
                SpawnBulletTrail(t_hit.point);
                //GameObject t_newHole = Instantiate(bulletHolePrefab, t_hit.point + t_hit.normal * 0.0001f, Quaternion.identity) as GameObject;
                //t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                //Destroy(t_newHole, 5f);

                if (loadout[currentIndex].isProjectile)
                {

                    Transform shootPoint = currentWeapon.transform.Find("ShootPoint");
                    
                    GameObject currentBullet = Instantiate(loadout[currentIndex].projectilePrefab, shootPoint.position, Quaternion.identity);
                    currentBullet.transform.forward = t_bloom;
                    currentBullet.GetComponent<Rigidbody>().AddForce(t_bloom * loadout[currentIndex].projectileForce, ForceMode.Impulse);   
                }

                if (t_hit.collider.gameObject.layer == 12)
                {
                    if (tempDamage -(int)(Vector3.Distance(transform.position, t_hit.collider.transform.position) * 0.1f) <0)
                    {
                        tempDamage = 1;
                    }
                    else
                    {
                        tempDamage -= (int)(Vector3.Distance(transform.position, t_hit.collider.transform.position) * 0.1f);
                    }

                    if (t_hit.collider.gameObject.tag=="Boss")
                    {
                        if (t_hit.collider.gameObject.GetComponent<BossController>().health>0)
                        {
                            Instantiate(enemySparksImpact, t_hit.point, t_hit.transform.rotation);
                            t_hit.collider.gameObject.GetComponent<BossController>().TakeDamage(tempDamage + dmgModifier);
                        }
                    }
                    else
                    {
                        if (t_hit.collider.gameObject.GetComponent<EnemyController>().health > 0)
                        {
                            Instantiate(enemySparksImpact, t_hit.point, t_hit.transform.rotation);
                            t_hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(tempDamage + dmgModifier);
                        }
                    }
                  
                    hitmarkerImage.color = new Color(0, 1, 0, 1);

                  
                    // sfx.PlayOneShot(hitmarkerSound);
                    hitmarkerWait = 0.1f;
                   // popups.DamageDealt(tempDamage + dmgModifier , Vector3.Distance(transform.position, t_hit.collider.transform.position));
                }
                else
                {
                    Instantiate(environmentalImpact, t_hit.point, t_hit.transform.rotation);
                }
            }
            else
            {

                if (loadout[currentIndex].isProjectile)
                {

                    Transform shootPoint = currentWeapon.transform.Find("ShootPoint");

                    GameObject currentBullet = Instantiate(loadout[currentIndex].projectilePrefab, shootPoint.position, Quaternion.identity);
                    currentBullet.transform.forward = t_bloom;
                    currentBullet.GetComponent<Rigidbody>().AddForce(t_bloom * loadout[currentIndex].projectileForce, ForceMode.Impulse);
                    Debug.Log(this.GetComponent<Rigidbody>().velocity.y);

                }
            }
        }
        //SOUND
        //sfx.Stop();
        sfx.clip = loadout[currentIndex].gunshotSound;
        sfx.pitch = 1 - loadout[currentIndex].pitchRandomization + Random.Range(-loadout[currentIndex].pitchRandomization, loadout[currentIndex].pitchRandomization);
        sfx.volume = 0.2f;
        sfx.Play();


        //gun fx 
        
        currentWeapon.transform.Rotate(-loadout[currentIndex].recoil, 0, 0);
        currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].kickback;

   


    }

    public void aimFOV(float currentFOV)
    {
        if (currentFOV == 0)
        {
            this.GetComponent<Player>().normalCam.fieldOfView = Mathf.Lerp(this.GetComponent<Player>().normalCam.fieldOfView, this.GetComponent<Player>().baseFOV * loadout[currentIndex].fovModifier, Time.deltaTime * loadout[currentIndex].aimSpeed);
        }
        else
        {
            this.GetComponent<Player>().normalCam.fieldOfView = Mathf.Lerp(this.GetComponent<Player>().normalCam.fieldOfView, this.GetComponent<Player>().baseFOV * (currentFOV - loadout[currentIndex].fovModifier), Time.deltaTime * loadout[currentIndex].aimSpeed);

        }

    }
   

    public void RefreshAmmo(Transform pbar)
    {
        //int t_clip = loadout[currentIndex].GetClip();
        //int t_stache = loadout[currentIndex].GetStash();

        float t_ammo_ratio = (float)currentAmmo / (float)maxAmmo;

        if (currentAmmo<=0)
        {
            pbar.localScale = Vector3.Lerp(pbar.localScale, new Vector3(0, 1, 1), Time.deltaTime * 8f);
        }
        else
        {
            pbar.localScale = Vector3.Lerp(pbar.localScale, new Vector3(t_ammo_ratio, 1, 1), Time.deltaTime * 8f);
        }
        


    }


    //private void TakeDamage(p_damage)
    //{
    //    GetComponent<ENEMY>.TakeDamage(p_damage);
    //}

    public void unEquip()
    {
        if (currentWeapon != null)
        {

            if (isReloading)
            {
                StopCoroutine("Reload");
            }
            Destroy(currentWeapon);

        }
    }
    #endregion

    private void SpawnBulletTrail(Vector3 hitPoint)
    {
        if (isAiming)
        {
            Transform shootPointADS = currentWeapon.transform.Find("States/ADS/MuzzleFlashPointADS");
            GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, shootPointADS.position, Quaternion.identity);
            LineRenderer lineR = bulletTrailEffect.GetComponent<LineRenderer>();

            lineR.SetPosition(0, shootPointADS.position);
            lineR.SetPosition(1, hitPoint);
            Destroy(bulletTrailEffect, 2f);
        }
        else if (!isAiming)
        {
           
            Transform shootPoint = currentWeapon.transform.Find("States/Hip/MuzzleFlashPointHIP");
            GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, shootPoint.position, Quaternion.identity);
            LineRenderer lineR = bulletTrailEffect.GetComponent<LineRenderer>();

            Debug.Log(shootPoint);

            lineR.SetPosition(0, shootPoint.transform.position);
            lineR.SetPosition(1, hitPoint);

            Destroy(bulletTrailEffect, 2f);
        }
    }
    
}
