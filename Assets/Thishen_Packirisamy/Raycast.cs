using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    public float rayDistance;
    public Text interactText;
    RaycastHit Focus;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rayDistance,Color.magenta);

        if (Physics.Raycast(transform.position, transform.forward,out Focus, rayDistance))
        {
           
            if (Focus.collider.gameObject.tag == "Chest"&&Focus.collider.gameObject.GetComponent<ChestController>().open == false)
            {
                if (interactText.text == ""&& Focus.collider.gameObject.GetComponent<ChestController>().open==false)
                {
                    interactText.text = "Press E to open chest";
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Focus.collider.gameObject.GetComponent<ChestController>().Open();
                 
                }
            }
            else
            {
                if (interactText.text != "")
                {
                    interactText.text = "";
                }
            }
           

        }
        else {
            if (interactText.text != "")
            {
                interactText.text = "";
            }
        }
    }
}
