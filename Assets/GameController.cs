using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GenerateMap MapGenerator;
    private int stage = 0;
    public bool changingStage = false;
    public GameObject player;

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

       // Debug.Log(GetGridPos());
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

    public Vector3 GetGridPos()
    {
        Vector3 gridPos= new Vector3(0,0,0);
        gridPos.x= (int)(player.transform.position.x / MapGenerator.blockSize/2);
        gridPos.z = (int)(player.transform.position.z / MapGenerator.blockSize/2);
        gridPos.y = 0;
        return gridPos;
    }

    //public List<Vector3> GetSpawnPos()
    //{
    //    List<Vector3> PossibleSpawns = new List<Vector3>();
    //    Vector3 gridPos= GetGridPos();
    //    Vector3 spawnPos = new Vector3(0, 0, 0);
    //    for (int i = 0; i < MapGenerator.gridSizeX; i++)
    //    {
            
    //    }
    //    return PossibleSpawns;
    //}

}
