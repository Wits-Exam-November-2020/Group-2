using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GenerateMap MapGenerator;
    private int stage = 0;
    public bool changingStage = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public Wallet playerWallet;

    void Start()
    {
        playerWallet = new Wallet();
        //temporary
        playerWallet.nuts = 1001;
        playerWallet.bolts = 1002;
        playerWallet.cogs = 1003;
        NextStage();
    }

    private void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            NextStage();
        }
    }

    public void NextStage()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
        foreach (GameObject cell in cells)
        {
            Destroy(cell);
        }
        changingStage = true;
        stage++;
        MapGenerator.CreateMap(); 
    }


}
