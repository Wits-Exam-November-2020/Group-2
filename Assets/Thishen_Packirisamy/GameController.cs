using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GenerateMap MapGenerator;
    private int stage = 0;
    public bool changingStage = false;
    private GameObject player;

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

    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        NextStage();
    }

    private void Update()
    {
        //if (Input.GetKeyDown("n"))
        //{
        //    NextStage();
        //}


        if (Input.GetKeyDown("n"))
        {
            GetSpawnPos();
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

    public List<Vector3> GetSpawnPos()
    {
        List<Vector3> PossibleSpawns = new List<Vector3>();
        Vector3 gridPos = GetGridPos();
        Vector3 spawnPos = new Vector3(0, 0, 0);

        for (int i=-1;i<=1;i++)
        {
            for (int j =-1;j<=1;j++)
            {

                int xIndex = ((int)gridPos.x) + i;
                int zIndex = ((int)gridPos.z) + j;

                if (zIndex>=0&&zIndex<MapGenerator.gridSizeZ&& xIndex >= 0 && xIndex < MapGenerator.gridSizeX &&!(xIndex==gridPos.x&&zIndex==gridPos.z))
                {
                    Transform spawnPoint = MapGenerator.chunks[xIndex, zIndex].transform.Find("Spawn Point");
                    if (spawnPoint!= null)
                    {
                        PossibleSpawns.Add(spawnPoint.position);
                    }
                    
                }
                
            }
        }
        return PossibleSpawns;
    }

}
