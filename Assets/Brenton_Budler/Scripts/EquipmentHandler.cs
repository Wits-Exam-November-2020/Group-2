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



            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                previousWeaponIndex = this.GetComponent<Weapon>().currentIndex;
                this.GetComponent<Weapon>().unEquip();
                 t_newEquipment = Instantiate(currentEquipment, equipmentThrowPoint.position, equipmentThrowPoint.rotation) as GameObject;
                

            }


            if (Input.GetKey(KeyCode.Alpha6) && t_newEquipment != null)
            {
            t_newEquipment.transform.position = equipmentThrowPoint.position;


            }



            if (Input.GetKeyUp(KeyCode.Alpha6) && t_newEquipment!=null)
            {

                t_newEquipment.GetComponent<Rigidbody>().useGravity = true;
                Transform t_spawn = transform.Find("Cameras/Player Camera");


                Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
                t_bloom -= t_spawn.position;
                t_bloom.Normalize();

                t_newEquipment.transform.forward = t_bloom;
                t_newEquipment.GetComponent<Rigidbody>().AddForce(t_bloom * 20f, ForceMode.Impulse);

                this.GetComponent<Weapon>().Equip(previousWeaponIndex);
            }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (currentEquipmentIndex<2)
            {
                currentEquipmentIndex++;
            }
            else
            {
                currentEquipmentIndex = 0;
            }
        }


        
    }
}
