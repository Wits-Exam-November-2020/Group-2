using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTIme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Bye", 10);
    }


    public void Bye()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
