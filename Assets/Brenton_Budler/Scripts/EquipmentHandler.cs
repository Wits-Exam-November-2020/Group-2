using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour
{


    public  Transform equipmentThrowPoint;
    public GameObject grenade;
    public GameObject emp;
    public GameObject healer;
    private int previousWeaponIndex;
    private GameObject t_newEquipment;
    public int currentEquipmentIndex;
    private GameObject currentEquipment; 

    private bool isPressed;
    private bool isDown;
    private bool isReleased;

    private int grenadeAmount = 2;
    private int empAmount = 2;
    private int healthBeaconAmount = 1; 

    private void Start()
    {
        currentEquipmentIndex = 0; 
    }

    // Update is called once per frame
    void Update()
    {


        switch (currentEquipmentIndex)
        {
            case 0:
                currentEquipment = grenade;


                break;
            case 1:
                currentEquipment = emp;
                break;

            case 2:
                currentEquipment = healer;
                break;

        }


        #region Grenade

        if (grenadeAmount > 0) {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                currentEquipment = grenade;
                previousWeaponIndex = this.GetComponent<Weapon>().currentIndex;
                this.GetComponent<Weapon>().unEquip();
                t_newEquipment = Instantiate(currentEquipment, equipmentThrowPoint.position, equipmentThrowPoint.rotation) as GameObject;


            }


            if (Input.GetKey(KeyCode.Alpha1) && t_newEquipment != null)
            {
                t_newEquipment.transform.position = equipmentThrowPoint.position;


            }

        }

        if (Input.GetKeyUp(KeyCode.Alpha1) && t_newEquipment != null)
            {
                grenadeAmount -= 1;
                t_newEquipment.GetComponent<Rigidbody>().useGravity = true;
                Transform t_spawn = transform.Find("Cameras/Player Camera");


                Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
                t_bloom -= t_spawn.position;
                t_bloom.Normalize();

                t_newEquipment.transform.forward = t_bloom;
                t_newEquipment.GetComponent<Rigidbody>().AddForce(t_bloom * 20f, ForceMode.Impulse);

                this.GetComponent<Weapon>().Equip(previousWeaponIndex);
            t_newEquipment = null;
        }
      
        #endregion


        #region EMP

        if (empAmount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentEquipment = emp;
                previousWeaponIndex = this.GetComponent<Weapon>().currentIndex;
                this.GetComponent<Weapon>().unEquip();
                t_newEquipment = Instantiate(currentEquipment, equipmentThrowPoint.position, equipmentThrowPoint.rotation) as GameObject;
                empAmount--;

            }


            if (Input.GetKey(KeyCode.Alpha2) && t_newEquipment != null)
            {
                t_newEquipment.transform.position = equipmentThrowPoint.position;


            }


        }
        if (Input.GetKeyUp(KeyCode.Alpha2) && t_newEquipment != null)
            {

                t_newEquipment.GetComponent<Rigidbody>().useGravity = true;
                Transform t_spawn = transform.Find("Cameras/Player Camera");


                Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
                t_bloom -= t_spawn.position;
                t_bloom.Normalize();

                t_newEquipment.transform.forward = t_bloom;
                t_newEquipment.GetComponent<Rigidbody>().AddForce(t_bloom * 20f, ForceMode.Impulse);

                this.GetComponent<Weapon>().Equip(previousWeaponIndex);
            t_newEquipment = null;
        }
      
        #endregion


        #region Health Beacon
        if (healthBeaconAmount > 0 && this.transform.position.y<20)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentEquipment = healer;
                previousWeaponIndex = this.GetComponent<Weapon>().currentIndex;
                this.GetComponent<Weapon>().unEquip();
                t_newEquipment = Instantiate(currentEquipment, equipmentThrowPoint.position, equipmentThrowPoint.rotation) as GameObject;
                healthBeaconAmount--;

            }


            if (Input.GetKey(KeyCode.Alpha3) && t_newEquipment != null)
            {
                t_newEquipment.transform.position = equipmentThrowPoint.position;


            }
        }


            if (Input.GetKeyUp(KeyCode.Alpha3) && t_newEquipment != null)
            {

                t_newEquipment.GetComponent<Rigidbody>().useGravity = true;
                Transform t_spawn = transform.Find("Cameras/Player Camera");


                Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
                t_bloom -= t_spawn.position;
                t_bloom.Normalize();

                t_newEquipment.transform.forward = t_bloom;
                t_newEquipment.GetComponent<Rigidbody>().AddForce(t_bloom * 20f, ForceMode.Impulse);

                this.GetComponent<Weapon>().Equip(previousWeaponIndex);
            t_newEquipment = null;
            }
        

        #endregion



        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    if (currentEquipmentIndex<2)
        //    {
        //        currentEquipmentIndex++;
        //    }
        //    else
        //    {
        //        currentEquipmentIndex = 0;
        //    }
        //}



    }
}
