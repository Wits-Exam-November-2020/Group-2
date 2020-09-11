using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetHealthBeaconNumber : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amount;
    private GameObject player;
    public Image icon;


    private void Update()
    {
        player = GameObject.Find("Player(Clone)");
        amount.text = player.GetComponent<EquipmentHandler>().healthBeaconAmount.ToString();

        if (player.GetComponent<EquipmentHandler>().healthBeaconAmount == 0)
        {
            icon.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
        }
        else
        {
            icon.GetComponent<Image>().color = new Color32(255, 255, 225, 225);
        }
    }
}
