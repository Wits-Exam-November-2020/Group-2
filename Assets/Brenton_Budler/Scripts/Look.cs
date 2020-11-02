using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    #region Varaibles
    public static bool cursorLocked = true;

    public Transform player;
    public Transform cams;
    public Transform weapon;

    private float xSensitivity;
    private float ySensitivity;
    [HideInInspector]
    public static float mouseSensitivity;

    public float maxAngle;

    private Quaternion camCenter;
    #endregion

    #region Built-in Functions

    void Start()
    {
        cursorLocked = true;
        camCenter = cams.localRotation;       
    }

    // Update is called once per frame
    void Update()
    {
        xSensitivity = mouseSensitivity;
        ySensitivity = mouseSensitivity;
        SetY();
        SetX();
        UpdateCursorLock();
    }

    #endregion

    #region Private Methods

    void SetY()
    {
        float t_input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
        Quaternion t_delta = cams.localRotation * t_adj;

        if (Quaternion.Angle(camCenter, t_delta) < maxAngle)
        {
            cams.localRotation = t_delta;

        }
        weapon.rotation = cams.rotation;


    }

    void SetX()
    {
        float t_input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.up);
        Quaternion t_delta = player.localRotation * t_adj;
        player.localRotation = t_delta;
    }

    void UpdateCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.isPaused)
            {
                cursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.isPaused)
            {
                cursorLocked = true;
            }
        }

        #endregion

    }

}
