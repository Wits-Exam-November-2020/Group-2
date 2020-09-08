using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interacting : MonoBehaviour
{
    public float rayDistance;


    private Transform t_spawn;
    private RaycastHit Focus;

    private GameObject promptText;
    private GameObject currentChest;
    private GameObject player;

    public Powerup doubleDamage;
    public Powerup invincible;




    // Start is called before the first frame update
    void Start()
    {
        t_spawn = transform.Find("Cameras/Player Camera");

        promptText = GameObject.Find("HUD/PromptText");
        
    }

    // Update is called once per frame
    void Update()
    {
        
        


        Debug.DrawRay(t_spawn.position, t_spawn.forward * rayDistance, Color.magenta);

        if (Physics.Raycast(t_spawn.position, t_spawn.forward, out  Focus, rayDistance))
        {
            
            if (Focus.collider.gameObject.tag=="Chest"&& Focus.collider.gameObject.GetComponent<ChestController>().open == false)
            {

                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press Q to Open Chest ";

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Focus.collider.gameObject.GetComponent<ChestController>().Open();
                    currentChest = Focus.collider.gameObject;

                }
            }



            if (Focus.collider.gameObject.tag == "Pistol")
            {
                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press E to Equip Pistol";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentChest.GetComponent<ChestController>().DestroyPrefab();
                    this.gameObject.GetComponent<Weapon>().Equip(0);
                }
                
            }


            if (Focus.collider.gameObject.tag == "AR")
            {
                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press E to Equip AR";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentChest.GetComponent<ChestController>().DestroyPrefab();
                    this.gameObject.GetComponent<Weapon>().Equip(1);
                }

            }

            if (Focus.collider.gameObject.tag == "ShotGun")
            {
                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press E to Equip Shot Gun";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentChest.GetComponent<ChestController>().DestroyPrefab();
                    this.gameObject.GetComponent<Weapon>().Equip(2);
                }

            }

            if (Focus.collider.gameObject.tag == "SMG")
            {
                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press E to Equip SMG";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentChest.GetComponent<ChestController>().DestroyPrefab();
                    this.gameObject.GetComponent<Weapon>().Equip(3);
                }

            }

            if (Focus.collider.gameObject.tag == "RPG")
            {
                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press E to Equip RPG";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentChest.GetComponent<ChestController>().DestroyPrefab();
                    this.gameObject.GetComponent<Weapon>().Equip(4);
                }

            }

            if (Focus.collider.gameObject.tag == "Shield")
            {
                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press P to Pickup Shield";

                if (Input.GetKeyDown(KeyCode.P))
                {
                    player = GameObject.Find("Player(Clone)");
                    player.GetComponent<Player>().current_shield += 33;
                    Destroy(Focus.collider.gameObject);
                }

            }

            if (Focus.collider.gameObject.tag == "JetPackRecovery")
            {
                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press P to Pickup Jetpack Recovery Reduction";

                if (Input.GetKeyDown(KeyCode.P))
                {
                    player = GameObject.Find("Player(Clone)");
                    player.GetComponent<Player>().jetRecovery += (player.GetComponent<Player>().jetRecovery * 0.02f);
                    Destroy(Focus.collider.gameObject);
                }

            }

            if (Focus.collider.gameObject.tag == "SpeedIncrease")
            {
                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press P to Pickup Speed Increase";

                if (Input.GetKeyDown(KeyCode.P))
                {
                    player = GameObject.Find("Player(Clone)");
                    player.GetComponent<Player>().speed += (player.GetComponent<Player>().speed * 0.01f);
                    Destroy(Focus.collider.gameObject);
                }

            }

            if (Focus.collider.gameObject.tag == "DoubleDamage")
            {
                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press P to Pickup Double Damage";

                if (Input.GetKeyDown(KeyCode.P))
                {
                    player = GameObject.Find("Player(Clone)");
                    int ind = player.GetComponent<UsablePowerUps>().checkSpace();
                    if (ind != 4)
                    {
                        player.GetComponent<UsablePowerUps>().currentPowerups[ind] = doubleDamage;
                        Destroy(Focus.collider.gameObject);
                    }
                    
                }

            }

            if (Focus.collider.gameObject.tag == "Invincible")
            {
                promptText.GetComponent<UnityEngine.UI.Text>().text = "Press P to Pickup Invincibility";

                if (Input.GetKeyDown(KeyCode.P))
                {
                    player = GameObject.Find("Player(Clone)");
                    int ind = player.GetComponent<UsablePowerUps>().checkSpace();
                    if (ind != 4)
                    {
                        player.GetComponent<UsablePowerUps>().currentPowerups[ind] = invincible;
                        Destroy(Focus.collider.gameObject);
                    }

                }

            }


        }
        else
        {
            promptText.GetComponent<UnityEngine.UI.Text>().text = "";
        }



    }


}
