using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPurchased : MonoBehaviour
{
    private GameObject player;
    public int weaponCost;
    


    private void OnDestroy()
    {
        player = GameObject.Find("Player(Clone)");

        if (player.GetComponent<Player>().wallet - weaponCost>=0) {
            player.GetComponent<Player>().wallet -= weaponCost;
            if (weaponCost>0)
            {
                player.GetComponent<Weapon>().currentAmmo = player.GetComponent<Weapon>().maxAmmo;
            }
            
        }
        else
        {
            player.GetComponent<Interacting>().promptText.GetComponent<UnityEngine.UI.Text>().text = "Insufficient funds";
        }
    }
}
