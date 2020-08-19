using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpRotation : MonoBehaviour
{


    private Quaternion origin_rotation;

    // Start is called before the first frame update
    void Start()
    {
        origin_rotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, origin_rotation, Time.deltaTime * 1);
    }
}
