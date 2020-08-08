using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;
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
    }


}
