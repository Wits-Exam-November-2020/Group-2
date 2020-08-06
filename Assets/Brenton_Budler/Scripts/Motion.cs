using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{

    public float speed;
    public float sprintModifier;
    public float jumpForce;
    public float lengthOfSlide;
    public float slideModifier;
    public Camera normalCam;
    public Transform groundDetector;
    public LayerMask ground;

    private Rigidbody rig;
    private float baseFOV;
    private float sprintFOVModifier = 1.25f;
    private Vector3 cameraOrigin;

    private bool sliding;
    private float slide_time;
    private Vector3 slide_dir;

    private void Start()
    {
        baseFOV = normalCam.fieldOfView;
        cameraOrigin = normalCam.transform.localPosition;
        if(Camera.main)Camera.main.enabled = false;
        rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Input
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKey(KeyCode.Space);
        bool slide = Input.GetKey(KeyCode.R);

        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;
        bool isSliding = isSprinting && slide && !sliding;


        //Jumping 
        if (isJumping)
        {
            rig.AddForce(Vector3.up * jumpForce);
        }


        //Movement

        Vector3 t_direction = Vector3.zero;
        float t_adjustedSpeed = speed;

        if (!sliding) {

           t_direction = new Vector3(t_hmove, 0, t_vmove);
           t_direction.Normalize();
           t_direction = transform.TransformDirection(t_direction);

            if (isSprinting) { t_adjustedSpeed *= sprintModifier; }

        }   
        else
        {
            t_direction = slide_dir;
            t_adjustedSpeed *= slideModifier;
            slide_time -= Time.deltaTime;
            if (slide_time<=0)
            {
                sliding = false;
               // normalCam.transform.localPosition += Vector3.up  * 0.5f;
            }
        }

        Vector3 t_targetVelcotiy = t_direction * t_adjustedSpeed * Time.deltaTime;
        t_targetVelcotiy.y = rig.velocity.y;
        rig.velocity = t_targetVelcotiy;

        //Sliding 
        if (isSliding)
        {
            sliding = true;
            slide_dir = t_direction;
            slide_time = lengthOfSlide;
           // normalCam.transform.localPosition += Vector3.down * 0.5f;

        }


        //Camera Stuff
        if (sliding)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier * 1.25f, Time.deltaTime * 8f);
            normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, cameraOrigin + Vector3.down * 1f, Time.deltaTime * 6f);


        }
        else
        {
            if (isSprinting) { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f); }
            else { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f); }

            normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, cameraOrigin, Time.deltaTime * 6f);
        }




    }
}
