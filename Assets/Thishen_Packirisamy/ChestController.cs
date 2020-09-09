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
    public Animator anim;
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
            item = Instantiate(itemPrefab, transform.position , this.transform.rotation );
            item.transform.Rotate(0, 90, 0);
            StartCoroutine(Move(transform.position + new Vector3(0, 1, 0)));
            open = true;
            openSound.Play();
            anim.SetTrigger("Open");
          //  GameController.instance.playerWallet= GameController.instance.playerWallet - price;
            
        }
        else
        {
            Debug.Log("Insufficient funds");
           
        }
        
    }

    IEnumerator Move(Vector3 target)//lerps cards position to the target
    {
        float timeBeforeStart = 0;
        float timeSinceStart = 0;
        float waitTime = 3f;
        
        
        while (timeBeforeStart < 0.8f)
        {
            timeBeforeStart += Time.deltaTime;
            yield return null;
        }
        while (timeSinceStart < waitTime)
        {
            timeSinceStart += Time.deltaTime;
            item.transform.position = Vector3.Lerp(item.transform.position, target, 0.005f);

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
