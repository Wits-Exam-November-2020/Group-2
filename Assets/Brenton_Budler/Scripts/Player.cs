using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    public float speed;
    public float sprintModifier;
    public float crouchModifier;
    public float jumpForce;
    public float jetForce;
    public float jetWait;
    public float jetRecovery;
    public float lengthOfSlide;

    public float max_fuel;
    public int max_health;
    private int current_health;

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
    private bool canJet;

    private Transform ui_fuelbar;
    private Transform ui_healthbar;

    private float current_fuel;
    private float current_recovery;


    private GameManager manager;
    #endregion

    #region Built-in Functions
    private void Start()
    {

        manager = GameObject.Find("Manager").GetComponent<GameManager>();


        current_health = max_health;

        current_fuel = max_fuel;

        baseFOV = normalCam.fieldOfView;
        if (Camera.main) Camera.main.enabled = false;
        cameraOrigin = normalCam.transform.localPosition;
        rig = GetComponent<Rigidbody>();

        weaponParentOrigin = weaponParent.localPosition;
        weaponParentCurrentPos = weaponParentOrigin;


        // ui_fuelbar = GameObject.Find("HUD/Fuel/Bar").transform;

        ui_healthbar = GameObject.Find("HUD/Health/Bar").transform;
        ui_fuelbar = GameObject.Find("HUD/Fuel/Bar").transform;
        UpdateHealthBar();
    }

    private void Update()
    {

     
        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(250);
        }
        
        //Input
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool slide = Input.GetKeyDown(KeyCode.C);
        bool crouch = Input.GetKeyDown(KeyCode.C);
        bool jet = Input.GetKey(KeyCode.Space);


        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.15f, ground);
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
            current_recovery = 0f;
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

        //UI REfreshes
        UpdateHealthBar();
    }

    void FixedUpdate()
    {
        //Input
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool slide = Input.GetKeyDown(KeyCode.C);
        bool jet = Input.GetKey(KeyCode.Space);

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


        //Jetting

        if (jump && !isGrounded)
        {
            canJet = true;
        }
        if (isGrounded)
        {
            canJet = false;
        }

        if (canJet && jet && current_fuel >0)
        {
            rig.AddForce(Vector3.up * jetForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            current_fuel = Mathf.Max(0, current_fuel - Time.deltaTime);

        }


        if (isGrounded)
        {
            if (current_recovery < jetWait)
            {
                current_recovery = Mathf.Max(jetWait, current_recovery + Time.fixedDeltaTime);
            }
            else
            {
                current_fuel = Mathf.Min(max_fuel, current_fuel + Time.fixedDeltaTime * jetRecovery);
            }
        }

        ui_fuelbar.localScale = new Vector3(current_fuel / max_fuel,1,1);


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

    void UpdateHealthBar()
    {
        float t_health_ratio = (float)current_health / (float)max_health;
        ui_healthbar.localScale = Vector3.Lerp(ui_healthbar.localScale,new Vector3(t_health_ratio, 1, 1),Time.deltaTime*8f);
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

    public void TakeDamage(int p_damage)
    {
        current_health -= p_damage;
        Debug.Log(current_health);

        UpdateHealthBar();
        if (current_health<0)
        {
            manager.Spawn();
            Destroy(gameObject);
        }
    }








}
