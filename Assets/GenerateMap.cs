using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        seed = Random.Range(0, 1f);
        teleporterGridPos.x = Random.Range(0, gridSizeX-1);
        teleporterGridPos.y = Random.Range(0, gridSizeZ-1);
        for (int i = 0;i<gridSizeX;i++)
        {
            for (int j = 0; j < gridSizeZ; j++)
            {
                if (teleporterGridPos.x== i && teleporterGridPos.y == j)
                {
                    currentPrefab = TPBlockPrefab;
                }
                else
                {
                    
                    currentPrefab = genericBlockPrefabs[chooseBlock(i,j)];
                }

                
                Instantiate(currentPrefab, new Vector3(25+i*blockSize*2,0.1f,25+j*blockSize*2), Quaternion.identity);
            }
        }
    }
    private int chooseBlock(int i, int j)
    {

        float iPer = (float)i / gridSizeX * perlScale;
        float jPer = (float)j / gridSizeZ * perlScale;
        
        //Debug.Log(iPer);
        //Debug.Log(jPer);
        //Debug.Log(" ");
        float perl = Mathf.PerlinNoise(seed+iPer, seed+jPer);
        Debug.Log(perl*10);
        int blockIndex = (int)(perl * 10);

        Debug.Log(blockIndex);
        return blockIndex;
        
    }

    void Update()
    {
        
    }
}
