using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject testChestPrefab;
    public GameObject testItemPrefab;
    private GameObject chest;
    private Wallet testPrice =new Wallet();
    // Start is called before the first frame update
    void Start()
    {
        GameObject chest = Instantiate(testChestPrefab, new Vector3(5.82f,1,5.49f), Quaternion.identity);
        chest.GetComponent<ChestController>().itemPrefab = testItemPrefab;
        testPrice.nuts = 1001;
        testPrice.bolts = 1002;
        testPrice.cogs = 1003;
        
        chest.GetComponent<ChestController>().price = testPrice;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
