using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpController : MonoBehaviour
{


    public Transform damagePopup;


    public void DamageDealt(int damageAmount , float distance)
    {

        
        Transform popup = Instantiate(damagePopup, Vector3.zero, Quaternion.identity);
        //popup.parent = GameObject.Find("Canvas/HUD").transform;
        //popup.localPosition = new Vector3(0, 0, 0);

        TextMeshProUGUI popupText = popup.GetComponentInChildren<TextMeshProUGUI>();
        popupText.SetText(damageAmount.ToString());

        popupText.fontSize = (1/distance)*(300+damageAmount);

        
       


    }
}
