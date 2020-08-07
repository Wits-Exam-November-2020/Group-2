using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    #region Variables
    public float speed;
    public float sprintModifier;
    public float crouchModifier;
    public float jumpForce;
    public float lengthOfSlide;
    public float slideModifier;
    public Camera normalCam;
    public Transform weaponParent;
    public Transform groundDetector;
    public LayerMask ground;

    public float slideAmount;
    public float crouchAmount;
    public GameObject standingCollider;
    public GameObject crouchingCollider; 

    private Rigidbody rig;

    private Vector3 targetWeaponBobPosition;
    private Vector3 weaponParentOrigin;
    private Vector3 weaponParentCurrentPos;

    private float movementCounter;
    private float idleCounter;

    private float baseFOV;
    private float sprintFOVModifier = 1.25f;
    private Vector3 cameraOrigin;

    private bool sliding;
    private float slide_time;
    private Vector3 slide_dir;
    private Vector3 t_direction = Vector3.zero;

    private bool crouched;
    #endregion

    #region Built-in Functions
    private void Start()
    {
        baseFOV = normalCam.fieldOfView;
        if (Camera.main) Camera.main.enabled = false;
        cameraOrigin = normalCam.transform.localPosition;
        rig = GetComponent<Rigidbody>();

        weaponParentOrigin = weaponParent.localPosition;
        weaponParentCurrentPos = weaponParentOrigin;
    }

    private void Update()
    {
        //Input
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool slide = Input.GetKeyDown(KeyCode.C);
        bool crouch = Input.GetKeyDown(KeyCode.C);


        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.2f, ground);
        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;
        bool isSliding = isSprinting && slide && !sliding;
        bool isCrouching = crouch && !isSprinting && !isJumping && isGrounded;

        //Crouching
        if (isCrouching)
        {
            SetCrouch(!crouched);
        }


        //Jumping 
        if (isJumping)
        {
            if (crouched) { SetCrouch(false); }
            rig.AddForce(Vector3.up * jumpForce);
        }

        //Head Bob 
        if (sliding)
        {
            //sliding
            HeadBob(movementCounter, 0.015f, 0.015f);
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);

        }
        else if (t_hmove ==0 && t_vmove==0)
        {
            //Idling
            HeadBob(idleCounter, 0.025f, 0.025f);
            idleCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
        }
        else if(!isSprinting &&!crouched){
            
            //Walking
            HeadBob(movementCounter, 0.035f, 0.035f);
            movementCounter += Time.deltaTime * 3f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6f);
        }
        else if (crouched)
        {
            //crouching
            HeadBob(movementCounter, 0.02f, 0.02f);
            movementCounter += Time.deltaTime * 1.75f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6f);

        }
        else
        {
            //sprinting 
            HeadBob(movementCounter, 0.15f, 0.075f);
            movementCounter += Time.deltaTime * 7f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
        }



        //Sliding 
        if (isSliding)
        {
            sliding = true;
            slide_dir = t_direction;
            slide_time = lengthOfSlide;
            weaponParentCurrentPos += Vector3.down * (slideAmount - crouchAmount);
            if (!crouched) { SetCrouch(true); }

        }
    }

    void FixedUpdate()
    {
        //Input
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKey(KeyCode.Space);
        bool slide = Input.GetKeyDown(KeyCode.C);

        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;
        bool isSliding = isSprinting && slide && !sliding;

        //Movement

        float t_adjustedSpeed = speed;

        if (!sliding) {

            t_direction = new Vector3(t_hmove, 0, t_vmove);
            t_direction.Normalize();
            t_direction = transform.TransformDirection(t_direction);

            if (isSprinting)
            {
                if (crouched) { SetCrouch(false); }
                t_adjustedSpeed *= sprintModifier;
            }
            else if(crouched)
            {
                t_adjustedSpeed *= crouchModifier;
            }

        }
        else
        {
            t_direction = slide_dir;
            t_adjustedSpeed *= slideModifier;
            slide_time -= Time.deltaTime;
            if (slide_time <= 0)
            {
                sliding = false;
               weaponParentCurrentPos += Vector3.up  *(slideAmount - crouchAmount);
            }
        }

        Vector3 t_targetVelcotiy = t_direction * t_adjustedSpeed * Time.deltaTime;
        t_targetVelcotiy.y = rig.velocity.y;
        rig.velocity = t_targetVelcotiy;



        //Camera Stuff
        if (sliding)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier * 1.25f, Time.deltaTime * 8f);
            normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, cameraOrigin + Vector3.down * slideAmount, Time.deltaTime * 6f);


        }
        else
        {
            if (isSprinting) { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f); }
            else { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f); }

            if (crouched) { normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, cameraOrigin + Vector3.down * crouchAmount, Time.deltaTime * 6f); }
            else { normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, cameraOrigin, Time.deltaTime * 6f); }
            
        }

    }


    #endregion


    void HeadBob(float p_z, float p_x_intensity, float p_y_intensity)
    {
        targetWeaponBobPosition = weaponParentCurrentPos + new Vector3(Mathf.Cos(p_z) * p_x_intensity, Mathf.Sin(p_z*2) * p_y_intensity, 0);

    }

    void SetCrouch(bool p_state)
    {
        if (crouched == p_state) { return; }

        crouched = p_state;

        if (crouched)
        {
            standingCollider.SetActive(false);
            crouchingCollider.SetActive(true);
            weaponParentCurrentPos += Vector3.down * crouchAmount;
        }
        else
        {
            standingCollider.SetActive(true);
            crouchingCollider.SetActive(false);
            weaponParentCurrentPos += Vector3.up * crouchAmount;
        }
    }








}
