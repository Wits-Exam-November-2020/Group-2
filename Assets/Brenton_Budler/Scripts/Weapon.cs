﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Varaibles 
    public Gun[] loadout;
    public Transform weaponParent;

    private int currentIndex;
    private GameObject currentWeapon;
    #endregion

    #region Built-in Functions

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }

        if (currentWeapon!=null)
        {
            Aim(Input.GetMouseButton(1));
        }

        
    }

    #endregion

    #region Own Methods
    void Equip(int p_ind)
    {

        if (currentWeapon != null) { Destroy(currentWeapon); }

        currentIndex = p_ind;

        GameObject t_newEquipment = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        t_newEquipment.transform.localPosition = Vector3.zero;
        t_newEquipment.transform.localEulerAngles = Vector3.zero;

        currentWeapon = t_newEquipment;
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
    #endregion
    
}
