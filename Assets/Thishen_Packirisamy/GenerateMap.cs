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
    private int randBlockNum;
    private Vector2 teleporterGridPos;
    private GameObject currentPrefab;
    private float seed;
    public GameObject[,] chunks;

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

                Debug.Log(currentPrefab);
                chunks[i,j] =Instantiate(currentPrefab, new Vector3(blockSize + i * blockSize * 2, 0.1f, blockSize + j * blockSize * 2), Quaternion.identity);

                int rand =Random.Range(1, 4);
                chunks[i,j].transform.Rotate(new Vector3(0,rand*90,0));
                
            }
        } 
    }

    

  
}
