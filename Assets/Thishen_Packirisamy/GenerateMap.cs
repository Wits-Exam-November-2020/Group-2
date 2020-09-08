using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GenerateMap : MonoBehaviour
{
    public GameObject[] genericBlockPrefabs = new GameObject[10];
    public GameObject TPBlockPrefab;
    public int gridSizeX;
    public int gridSizeZ;
    public float blockSize;
    public float perlScale = 2f;
    public GameObject powerUpChestPrefab;
    public List<GameObject> powerUps;
    public Wallet powerUpsPrice;
    public GameObject weaponChestPrefab;
    public Wallet weaponsPrice;
    public List<GameObject> weapons;
    private int randBlockNum;
    private Vector2 teleporterGridPos;
    private GameObject currentPrefab;
    private float seed;
    public GameObject[,] chunks;
    public GameObject wallPrefab;

    void Start()
    {
        chunks = new GameObject[gridSizeX,gridSizeZ];
    }

    private int chooseBlock(int i, int j)
    {

        float iPer = (float)i / gridSizeX * perlScale;
        float jPer = (float)j / gridSizeZ * perlScale;
     
        float perl = Mathf.PerlinNoise(seed+iPer, seed+jPer);
        int blockIndex = (int)(perl * 10);

        return blockIndex;
    }

    public void CreateMap()
    {
        PlaceWalls();
        seed = Random.Range(0, 1f);
        teleporterGridPos.x = Random.Range(0, gridSizeX - 1);
        teleporterGridPos.y = Random.Range(0, gridSizeZ - 1);
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeZ; j++)
            {
                if (teleporterGridPos.x == i && teleporterGridPos.y == j)
                {
                    currentPrefab = TPBlockPrefab;
                }
                else
                {

                    currentPrefab = genericBlockPrefabs[chooseBlock(i, j)];
                }

                chunks[i,j] =Instantiate(currentPrefab, new Vector3(blockSize + i * blockSize * 2, 0.1f, blockSize + j * blockSize * 2), Quaternion.identity);
                int rand = Random.Range(1, 4);
                chunks[i, j].transform.Rotate(new Vector3(0, rand * 90, 0));
                Transform ChestSpawnPoint = chunks[i,j].transform.Find("ChestSpawnPoint");
                //Debug.Log(ChestSpawnPoint);
                if (ChestSpawnPoint != null)
                {
                    SpawnChest(ChestSpawnPoint.position,ChestSpawnPoint.rotation);
                    Debug.Log(ChestSpawnPoint.position);
                }
                
                
            }
        } 
    }

    private void PlaceWalls()
    {

        
     
        GameObject wall1 = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        wall1.transform.Rotate(new Vector3(0,  -90, 0));
        wall1.transform.localScale = new Vector3(gridSizeZ * blockSize*2, wall1.transform.localScale.y, wall1.transform.localScale.z);

        GameObject wall2 = Instantiate(wallPrefab, new Vector3(gridSizeX*blockSize*2,0,0), Quaternion.identity);
        wall2.transform.Rotate(new Vector3(0, -90, 0));
        wall2.transform.localScale = new Vector3(gridSizeZ * blockSize*2, wall2.transform.localScale.y, wall2.transform.localScale.z);

        GameObject wall3 = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        wall3.transform.Rotate(new Vector3(0, 0, 0));
        wall3.transform.localScale = new Vector3(gridSizeX * blockSize*2, wall3.transform.localScale.y, wall3.transform.localScale.z);

        GameObject wall4 = Instantiate(wallPrefab, new Vector3(0, 0, gridSizeZ * blockSize*2), Quaternion.identity);
        wall4.transform.Rotate(new Vector3(0, 0, 0));
        wall4.transform.localScale = new Vector3(gridSizeX * blockSize*2, wall4.transform.localScale.y, wall4.transform.localScale.z);


    }

    public void SpawnChest(Vector3 pos, Quaternion rotation)
    {
        int chestTypeSelector = Random.Range(0, 3);

        if (chestTypeSelector<2)
        {
            GameObject item = powerUps[Random.Range(0, powerUps.Count)];
            GameObject chest = Instantiate(powerUpChestPrefab, pos, Quaternion.identity);
            chest.transform.rotation = rotation;
            chest.GetComponent<ChestController>().itemPrefab = item;
            chest.GetComponent<ChestController>().price = powerUpsPrice;
        }
        else
        {
            GameObject item = weapons[Random.Range(0, weapons.Count)];
            GameObject chest = Instantiate(weaponChestPrefab, pos, Quaternion.identity);
            chest.transform.rotation = rotation;
            chest.GetComponent<ChestController>().itemPrefab = item;
            chest.GetComponent<ChestController>().price = weaponsPrice;
        }

    }

    

  
}
