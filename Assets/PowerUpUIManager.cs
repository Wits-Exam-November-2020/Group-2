using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUIManager : MonoBehaviour
{

    private int[] slots = { 0, 0, 0, 0 };
    public GameObject[] slot1Images;
    public Transform slot1Text;


    public GameObject[] slot2Images;
    public Transform slot2Text;

    public GameObject[] slot3Images;
    public Transform slot3Text;

    public GameObject[] slot4Images;
    public Transform slot4Text;


    private int damagePerc = 0;
    private int jetpackPerc = 0;
    private int speeedPerc = 0;
    private int ammoRedPerc = 0;

    public GameObject[] popScreens;
    private bool poppingUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void findSpace(int powerupIndex)
    {


        for (int i = 0; i <= 3; i++)
        {
            if (slots[i]==powerupIndex)
            {
                //INCREASE 
               
                    popScreens[powerupIndex - 1].SetActive(true);
                    Invoke("resetActive", 2);
                
                
                
                
                increaseAmount(i, powerupIndex);
                
                return;
            }
            else
            {
                if (slots[i]==0)
                {

                    slots[i] = powerupIndex;
                    popScreens[powerupIndex - 1].SetActive(true);
                    Invoke("resetActive", 2);
                    setChipDisplay(i, powerupIndex);
                    return;
                }
            }
        }
        
    }

    public void setChipDisplay(int slotIndex, int powerUp)
    {
        switch (slotIndex)
        {
            case 0:
                switch (powerUp)
                {
                    case 1: slot1Images[0].SetActive(true);
                        damagePerc += 10;
                        slot1Text.GetComponent<TMPro.TextMeshProUGUI>().text = damagePerc +"%";// DamageIncrease
                        break;
                    case 2: slot1Images[1].SetActive(true);
                        ammoRedPerc += 10;
                        slot1Text.GetComponent<TMPro.TextMeshProUGUI>().text = ammoRedPerc + "%"; //AmmoPriceReduction
                        break;
                    case 3: slot1Images[2].SetActive(true);
                        speeedPerc += 10; 
                        slot1Text.GetComponent<TMPro.TextMeshProUGUI>().text = speeedPerc+ "%"; //SpeedIncrease
                        break;
                    case 4: slot1Images[3].SetActive(true);
                        jetpackPerc += 10; 
                        slot1Text.GetComponent<TMPro.TextMeshProUGUI>().text = jetpackPerc + "%"; //JetPackRecovery
                        break;
                }

                break;
            case 1:
                switch (powerUp)
                {
                    case 1:
                        slot2Images[0].SetActive(true);
                        damagePerc += 10;
                        slot2Text.GetComponent<TMPro.TextMeshProUGUI>().text = damagePerc + "%";// DamageIncrease
                        break;
                    case 2:
                        slot2Images[1].SetActive(true);
                        ammoRedPerc += 10;
                        slot2Text.GetComponent<TMPro.TextMeshProUGUI>().text = ammoRedPerc + "%"; //AmmoPriceReduction
                        break;
                    case 3:
                        slot2Images[2].SetActive(true);
                        speeedPerc += 10;
                        slot2Text.GetComponent<TMPro.TextMeshProUGUI>().text = speeedPerc + "%"; //SpeedIncrease
                        break;
                    case 4:
                        slot2Images[3].SetActive(true);
                        jetpackPerc += 10;
                        slot2Text.GetComponent<TMPro.TextMeshProUGUI>().text = jetpackPerc + "%"; //JetPackRecovery
                        break;
                }
                break;
            case 2:
                switch (powerUp)
                {
                    case 1:
                        slot3Images[0].SetActive(true);
                        damagePerc += 10;
                        slot3Text.GetComponent<TMPro.TextMeshProUGUI>().text = damagePerc + "%";// DamageIncrease
                        break;
                    case 2:
                        slot3Images[1].SetActive(true);
                        ammoRedPerc += 10;
                        slot3Text.GetComponent<TMPro.TextMeshProUGUI>().text = ammoRedPerc + "%"; //AmmoPriceReduction
                        break;
                    case 3:
                        slot3Images[2].SetActive(true);
                        speeedPerc += 10;
                        slot3Text.GetComponent<TMPro.TextMeshProUGUI>().text = speeedPerc + "%"; //SpeedIncrease
                        break;
                    case 4:
                        slot3Images[3].SetActive(true);
                        jetpackPerc += 10;
                        slot3Text.GetComponent<TMPro.TextMeshProUGUI>().text = jetpackPerc + "%"; //JetPackRecovery
                        break;
                }
                break;
            case 3:
                switch (powerUp)
                {
                    case 1:
                        slot4Images[0].SetActive(true);
                        damagePerc += 10;
                        slot4Text.GetComponent<TMPro.TextMeshProUGUI>().text = damagePerc + "%";// DamageIncrease
                        break;
                    case 2:
                        slot4Images[1].SetActive(true);
                        ammoRedPerc += 10;
                        slot4Text.GetComponent<TMPro.TextMeshProUGUI>().text = ammoRedPerc + "%"; //AmmoPriceReduction
                        break;
                    case 3:
                        slot4Images[2].SetActive(true);
                        speeedPerc += 10;
                        slot4Text.GetComponent<TMPro.TextMeshProUGUI>().text = speeedPerc + "%"; //SpeedIncrease
                        break;
                    case 4:
                        slot4Images[3].SetActive(true);
                        jetpackPerc += 10;
                        slot4Text.GetComponent<TMPro.TextMeshProUGUI>().text = jetpackPerc + "%"; //JetPackRecovery
                        break;
                }
                break;
        }
    }

    public void increaseAmount(int slotIndex ,int powerUp)
    {
        switch (slotIndex)
        {
            case 0:
                switch (powerUp)
                {
                    case 1:
                        
                        damagePerc += 10;
                        slot1Text.GetComponent<TMPro.TextMeshProUGUI>().text = damagePerc + "%";// DamageIncrease
                        break;
                    case 2:
                       
                        ammoRedPerc += 10;
                        slot1Text.GetComponent<TMPro.TextMeshProUGUI>().text = ammoRedPerc + "%"; //AmmoPriceReduction
                        break;
                    case 3:
                      
                        speeedPerc += 10;
                        slot1Text.GetComponent<TMPro.TextMeshProUGUI>().text = speeedPerc + "%"; //SpeedIncrease
                        break;
                    case 4:
                       
                        jetpackPerc += 10;
                        slot1Text.GetComponent<TMPro.TextMeshProUGUI>().text = jetpackPerc + "%"; //JetPackRecovery
                        break;
                }

                break;
            case 1:
                switch (powerUp)
                {
                    case 1:
                        
                        damagePerc += 10;
                        slot2Text.GetComponent<TMPro.TextMeshProUGUI>().text = damagePerc + "%";// DamageIncrease
                        break;
                    case 2:
                        
                        ammoRedPerc += 10;
                        slot2Text.GetComponent<TMPro.TextMeshProUGUI>().text = ammoRedPerc + "%"; //AmmoPriceReduction
                        break;
                    case 3:
                        
                        speeedPerc += 10;
                        slot2Text.GetComponent<TMPro.TextMeshProUGUI>().text = speeedPerc + "%"; //SpeedIncrease
                        break;
                    case 4:
                       
                        jetpackPerc += 10;
                        slot2Text.GetComponent<TMPro.TextMeshProUGUI>().text = jetpackPerc + "%"; //JetPackRecovery
                        break;
                }
                break;
            case 2:
                switch (powerUp)
                {
                    case 1:
                        
                        damagePerc += 10;
                        slot3Text.GetComponent<TMPro.TextMeshProUGUI>().text = damagePerc + "%";// DamageIncrease
                        break;
                    case 2:
                        
                        ammoRedPerc += 10;
                        slot3Text.GetComponent<TMPro.TextMeshProUGUI>().text = ammoRedPerc + "%"; //AmmoPriceReduction
                        break;
                    case 3:
                        
                        speeedPerc += 10;
                        slot3Text.GetComponent<TMPro.TextMeshProUGUI>().text = speeedPerc + "%"; //SpeedIncrease
                        break;
                    case 4:
                        
                        jetpackPerc += 10;
                        slot3Text.GetComponent<TMPro.TextMeshProUGUI>().text = jetpackPerc + "%"; //JetPackRecovery
                        break;
                }
                break;
            case 3:
                switch (powerUp)
                {
                    case 1:
                        
                        damagePerc += 10;
                        slot4Text.GetComponent<TMPro.TextMeshProUGUI>().text = damagePerc + "%";// DamageIncrease
                        break;
                    case 2:
                        
                        ammoRedPerc += 10;
                        slot4Text.GetComponent<TMPro.TextMeshProUGUI>().text = ammoRedPerc + "%"; //AmmoPriceReduction
                        break;
                    case 3:
                        
                        speeedPerc += 10;
                        slot4Text.GetComponent<TMPro.TextMeshProUGUI>().text = speeedPerc + "%"; //SpeedIncrease
                        break;
                    case 4:
                       
                        jetpackPerc += 10;
                        slot4Text.GetComponent<TMPro.TextMeshProUGUI>().text = jetpackPerc + "%"; //JetPackRecovery
                        break;
                }
                break;
        }
    }

    public void playHealth()
    {
        popScreens[4].SetActive(true);
        Invoke("resetActive", 2);

    }

    public void playShield()
    {
        popScreens[5].SetActive(true);
        Invoke("resetActive", 2);

    }

    public void resetActive()
    {
        for (int i = 0; i <=5; i++)
        {
            if (popScreens[i].activeSelf)
            {
                popScreens[i].SetActive(false);
            }
        }
    }
}
