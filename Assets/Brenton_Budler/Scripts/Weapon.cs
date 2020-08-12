using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Varaibles 
    public Gun[] loadout;
    public Transform weaponParent;
    public GameObject bulletHolePrefab;
    public LayerMask canBeShot;

    private float currentCooldown;
    private int currentIndex;
    private GameObject currentWeapon;
    #endregion

    #region Built-in Functions

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
          //  Equip(0);
        }

        if (currentWeapon!=null)
        {
            Aim(Input.GetMouseButton(1));
            if (Input.GetMouseButtonDown(0) && currentCooldown<=0)
            {
                Shoot();
            }

            //weapon position elasticity 
            currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime*4f);

            //cooldown for weapon
            if (currentCooldown>0){ currentCooldown -= Time.deltaTime; }


        }

        
    }

    #endregion

    #region Own Methods



    public void Equip(int p_ind)
    {

        if (currentWeapon != null) { Destroy(currentWeapon); }

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
        Transform t_spawn = transform.Find("Cameras/Player Camera");

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
            GameObject t_newHole = Instantiate(bulletHolePrefab, t_hit.point + t_hit.normal*0.0001f, Quaternion.identity) as GameObject;
            t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
            Destroy(t_newHole, 5f);

            //if (t_hit.collider.gameobject.layer == 11)
            //{
            //    //damage enemy

                    // ENEMY.TAKEDAMAGE(loadout[currentIndex].damage)
            //}
        }

        //gun fx 
        currentWeapon.transform.Rotate(-loadout[currentIndex].recoil, 0, 0);
        currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].kickback;

        //cooldown
        currentCooldown = loadout[currentIndex].firerate;

    }


    //private void TakeDamage(p_damage)
    //{
    //    GetComponent<ENEMY>.TakeDamage(p_damage);
    //}
    #endregion
    
}
