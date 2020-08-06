using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    #region Varaibles 
    public float intensity;
    public float smooth;

    private Quaternion origin_rotation;
    #endregion

    #region Built-in Functions

    private void Start()
    {
        origin_rotation = transform.localRotation;  
    }

    private void Update()
    {
        UpdateSway();
    }

    #endregion


    #region Own Methods
    private void UpdateSway()
    {
        //Controls
        float t_x_mouse = Input.GetAxis("Mouse X");
        float t_y_mouse = Input.GetAxis("Mouse Y"); 

        //Calculate target rotation
        Quaternion target_x_adj = Quaternion.AngleAxis(-intensity*t_x_mouse, Vector3.up);
        Quaternion target_y_adj = Quaternion.AngleAxis(intensity * t_y_mouse, Vector3.right);
        Quaternion target_rotation = origin_rotation * target_x_adj* target_y_adj; 

        //Rotate towards target rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);

    }
    #endregion
}
