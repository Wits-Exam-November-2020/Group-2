using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] genericBlockPrefabs;
    public GameObject TPBlockPrefab;
    public int gridSizeX;
    public int gridSizeZ;
    public float blockSize;
    private int randBlockNum;
    private Vector2 teleporterGridPos;
    private GameObject currentPrefab;
    void Start()
    {
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
                    randBlockNum = Random.Range(0, genericBlockPrefabs.Length);
                    currentPrefab = genericBlockPrefabs[randBlockNum];
                }

                
                Instantiate(currentPrefab, new Vector3(25+i*blockSize*2,0.1f,25+j*blockSize*2), Quaternion.identity);
            }
        }
    }

    void Update()
    {
        
    }
}
