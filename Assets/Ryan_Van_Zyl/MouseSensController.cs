using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensController : MonoBehaviour
{
    public Slider mouseSenseSlider;


    // Start is called before the first frame update
    void start  ()
    {
        
        mouseSenseSlider.maxValue = 200f;
        mouseSenseSlider.value = Look.mouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        Look.mouseSensitivity = mouseSenseSlider.value;
    }
}
