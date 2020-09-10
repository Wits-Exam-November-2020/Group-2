using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetNumber : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amount;
    private GameObject player;
    public Image icon;


    private void Update()
    {
        player = GameObject.Find("Player(Clone)");
        amount.text = player.GetComponent<EquipmentHandler>().grenadeAmount.ToString();

        if (player.GetComponent<EquipmentHandler>().grenadeAmount==0)
        {
            icon.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
        }
        else
        {
            icon.GetComponent<Image>().color = new Color32(255, 255, 225, 225);
        }
    }
}
