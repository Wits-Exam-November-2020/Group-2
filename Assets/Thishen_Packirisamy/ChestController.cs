using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ChestController : MonoBehaviour
{
    public GameObject itemPrefab;
    private GameObject item;
    public bool open=false;
    public Wallet price;
    private AudioSource openSound;
    // Start is called before the first frame update
    void Start()
    {
        openSound = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
       
    }

    public void Open()
    {
       
        // Debug.Log("OPENING");
        if (GameController.instance.playerWallet>=price)
        {
            item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            StartCoroutine(Move(transform.position + new Vector3(0, 1, 0)));
            open = true;
            openSound.Play();
           
            GameController.instance.playerWallet= GameController.instance.playerWallet - price;
            
        }
        else
        {
            Debug.Log("Insufficient funds");
           
        }
        
    }

    IEnumerator Move(Vector3 target)//lerps cards position to the target
    {
        float timeSinceStart = 0;
        float waitTime = 1f;
        while (timeSinceStart < waitTime)
        {
            item.transform.position = Vector3.Lerp(item.transform.position, target, 0.015f);
            timeSinceStart += Time.deltaTime;
            yield return null;
        }
        item.transform.position = target;
        yield return null;
    }

    public void DestroyPrefab()
    {
        Destroy(item);
    }
}
