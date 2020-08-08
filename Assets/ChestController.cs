using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject itemPrefab;
    private GameObject item;
    // Start is called before the first frame update
    void Start()
    {
       item = Instantiate(itemPrefab, transform.position, Quaternion.identity); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        item.transform.position = transform.position + new Vector3(0, 1, 0);
    }
}
