using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Player : MonoBehaviour
{
    #region Variables
    public float speed;
    public float defaultSpeed;
    public float sprintModifier;
    public float crouchModifier;
    public float jumpForce;
    public float jetForce;
    public float jetWait;
    public float jetRecovery;
    public float lengthOfSlide;

    public float max_fuel;
    public int max_health;
    public int max_shield;
    public int current_health;
    public int current_shield;

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
    private Transform ui_shieldbar;
    private Text ui_ammo;

    private Transform ui_ammobar;




    private float current_fuel;
    private float current_recovery;


    private GameManager manager;
    private Weapon weapon;

    private Vector3 hookshotPosition;
    private bool hitGrap;
    private float hookShotSpeed;
    private Transform hookshotTransform;
    private float hookShotSize;
    private bool thrownState;

    private Vector3 characterVelocityMomentum;

    private bool jet;
    private bool jump;

    private Vector3 previousPosition;

    public bool invincible;

    public AudioSource jetpackActivation;
    public AudioSource jetpackUsing;
    public AudioSource walking;
    public AudioSource slidingSound;
    public AudioSource runningSound;


    private Image takeDamageImage;
    private float takeDamageWait;

    public int costOfAmmo;
    public int wallet;

    #endregion

    #region Built-in Functions


    private Text currencyText;


    private void Start()
    {


        wallet = 200;
        costOfAmmo = 50;
        defaultSpeed = speed;
        invincible = false;

        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        weapon = GetComponent<Weapon>();

        current_health = max_health;
        current_shield = 0;

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
        ui_ammobar = GameObject.Find("HUD/AmmoBar/Bar").transform;
        ui_shieldbar = GameObject.Find("HUD/Shield/Bar").transform;
        currencyText = GameObject.Find("HUD/Ammo/Text").GetComponent<Text>();

        takeDamageImage = GameObject.Find("HUD/DamagePanel").GetComponent<Image>();
        takeDamageImage.color = new Color(0, 0, 0, 0);
        

        UpdateHealthBar();
        UpdateShieldBar();
        //ui_ammo = GameObject.Find("HUD/Ammo/Text").GetComponent<Text>();


        hookshotTransform = GameObject.Find("RopeHolder").transform;
       // hookshotTransform.gameObject.SetActive(false);
        thrownState = false;

        previousPosition = transform.position;
    }

    private void Update()
    {
        weapon.RefreshAmmo(ui_ammobar);
        //if (transform.position-previousPosition)
        //{
        //    previousPosition = transform.position;
        //    Debug.Log("Moving");
        //}
        //else
        //{
        //    rig.useGravity = true;
        //}
        currencyText.text = wallet.ToString();

        if (takeDamageImage.color.a > 0)
        {
            takeDamageImage.color = Color.Lerp(takeDamageImage.color, new Color(0, 0, 0, 0), Time.deltaTime * 1f);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (invincible==false)
            {
                invincible = true;
            }
            else if(invincible==true)
            {
                invincible = false;
            }
        }


        if (current_health>=max_health)
        {
            current_health = max_health;
        }

        if (current_shield>=max_shield)
        {
            current_shield = max_shield;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(25);
        }

        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //    current_shield += 33;
        //}

        //invincible = true;

        
        //Input
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        jump = Input.GetKeyDown(KeyCode.Space);
        bool slide = Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C);
        bool crouch = Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C);
        jet = Input.GetKey(KeyCode.Space);
        //   bool jet = Input.GetKey(KeyCode.Space);



        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.2f, ground);
        Debug.DrawRay(groundDetector.position, Vector3.down , Color.green);
        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;
        bool isSliding = isSprinting && slide && !sliding;
        bool isCrouching = crouch && !isSprinting && !isJumping && isGrounded;

        if (isSprinting )
        {
            if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)|| Input.GetKeyDown(KeyCode.C)) {
                slidingSound.Play();
            }
        }

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
        if (!isGrounded)
        {
            HeadBob(movementCounter, 0.01f, 0.01f);
            idleCounter += 0;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
        }
        else if (sliding)
        {
            //sliding
          
            HeadBob(movementCounter, 0.15f, 0.075f);
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

        if (isSprinting && !runningSound.isPlaying)
        {
                runningSound.Play();
                walking.Stop();

        }
        else
        {

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

        //Grappling Hook SHOOOT 

            HandleHookshotStart();



        //UI REfreshes
        UpdateHealthBar();
        UpdateShieldBar();
        weapon.RefreshAmmo(ui_ammobar);


        if (!isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canJet = true;
            }
        }




    }



    void FixedUpdate()
    {

        weapon.RefreshAmmo(ui_ammobar);

        //Input
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        // bool jump = Input.GetKeyDown(KeyCode.Space);
        bool slide = Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C);


        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.2f, ground);
        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;
        bool isSliding = isSprinting && slide && !sliding;
      

        //Movement
        float t_adjustedSpeed = speed;



        if (thrownState)
        {

            rig.useGravity = true;
            hookshotTransform.LookAt(hookshotPosition);
            float hookShotThrowSpeed = 70f;
            hookShotSize += hookShotThrowSpeed * Time.fixedDeltaTime;
            hookshotTransform.localScale = new Vector3(1, 1, hookShotSize);

            if (Vector3.Distance(transform.position, hookshotPosition) - hookShotSize < 0.5)
            {
                thrownState = false;
                hitGrap = true;
                hookshotTransform.localScale = Vector3.zero;
                hookShotSize=0;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                rig.useGravity = true;
                hitGrap = false;
                thrownState = false;
                hookshotTransform.localScale = Vector3.zero;
                hookShotSize=0;
            }
        }
        else if (hitGrap)
        {
            rig.useGravity = false;
            float hookshotSpeedMin = 2f;
            float hookshotSpeedMax = 4f; 
            hookShotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotPosition), hookshotSpeedMin,hookshotSpeedMax);
            
            transform.position = Vector3.Lerp(transform.position, hookshotPosition, Time.fixedDeltaTime * hookShotSpeed);

            
           

            if (Vector3.Distance(transform.position,hookshotPosition)<2f)
            {
                hitGrap = false;
            }

            Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rig.useGravity = true;
                hitGrap = false;
                thrownState = false;
                float momentumExtraSpeed = 7f;
                characterVelocityMomentum = hookshotDir * hookShotSpeed * momentumExtraSpeed;
                rig.AddForce(Vector3.up * jumpForce);
            }

        }
        else
        {

            rig.useGravity = true;
            if (!sliding)
            {

                t_direction = new Vector3(t_hmove, 0, t_vmove);
                t_direction.Normalize();
                t_direction = transform.TransformDirection(t_direction);

                if (isSprinting)
                {
                    if (crouched) { SetCrouch(false); }
                    t_adjustedSpeed *= sprintModifier;
                }
                else if (crouched)
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
                    weaponParentCurrentPos += Vector3.up * (slideAmount - crouchAmount);
                }
            }

            Vector3 t_targetVelcotiy = t_direction * t_adjustedSpeed * Time.deltaTime;
            t_targetVelcotiy += characterVelocityMomentum;
            t_targetVelcotiy.y = rig.velocity.y;
            rig.velocity = t_targetVelcotiy;

            if (characterVelocityMomentum.magnitude >=0f)
            {
                float momentumDrag = 3f;
                characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
                if (characterVelocityMomentum.magnitude<.0f)
                {
                    characterVelocityMomentum = Vector3.zero;
                }
            }
        }


        //Jetting

        if (!isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                canJet = true;
            }
        }
        if (isGrounded)
        {
            canJet = false;

            if (t_hmove!=0 || t_vmove!=0)
            {

         

                if (walking.isPlaying)
                {

                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) {
                        if (isGrounded)
                        {
                            walking.Play();
                        }
                        
                    }
                }
            }
            else
            {
                walking.Stop();
            }
   


            if (jetpackUsing.volume>0f)
            {
                jetpackUsing.volume -= 0.02f;
            }
            else
            {
                jetpackUsing.Stop();
                jetpackUsing.volume = 0.05f;
            }
            
        }

        if (canJet && jet && current_fuel >0)
        {
            if (!jetpackUsing.isPlaying)
            {
                jetpackUsing.Play();
            }
            
            jetpackUsing.volume += 0.009f;
            rig.AddForce(Vector3.up * jetForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            current_fuel = Mathf.Max(0, current_fuel - Time.deltaTime*1.5f);

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

            if (crouched) { normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, cameraOrigin + Vector3.down * crouchAmount, Time.deltaTime * 8f); }
            else { normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, cameraOrigin, Time.deltaTime * 8f); }

            if (hitGrap) { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f); }
            else { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f); }


        }

    }


    #endregion


    void HeadBob(float p_z, float p_x_intensity, float p_y_intensity)
    {
        float t_aim_adjust = 1f;
        if (weapon.isAiming)
        {
            t_aim_adjust = 0.1f;
        }
        targetWeaponBobPosition = weaponParentCurrentPos + new Vector3(Mathf.Cos(p_z) * p_x_intensity * t_aim_adjust, Mathf.Sin(p_z*2) * p_y_intensity * t_aim_adjust, 0);

    }

    void UpdateHealthBar()
    {
        float t_health_ratio = (float)current_health / (float)max_health;
        ui_healthbar.localScale = Vector3.Lerp(ui_healthbar.localScale,new Vector3(t_health_ratio, 1, 1),Time.deltaTime*8f);
    }

    void UpdateShieldBar()
    {
        float t_shield_ratio = (float)current_shield / (float)max_shield;
        ui_shieldbar.localScale = Vector3.Lerp(ui_shieldbar.localScale, new Vector3(t_shield_ratio, 1, 1), Time.deltaTime * 8f);
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
        takeDamageImage.color = new Color(1, 1, 1, 1);
        DamageSoundController.playDamageSound = true;
        

        if (!invincible)
        {
            if (p_damage < current_shield)
            {
                current_shield -= p_damage;
            }
            else
            {
                current_health -= (p_damage - current_shield);
                current_shield = 0;

            }
        }
        else
        {

        }

        UpdateHealthBar();
        UpdateShieldBar();
        if (current_health<=0)
        {
            SceneManager.LoadScene(0);
            GameController.instance.Spawn();
            current_health = max_health;
            Destroy(gameObject);
        }
    }

    public void Heal()
    {
        current_health += 1;
        UpdateHealthBar();
    }

 
    private void HandleHookshotStart()
    {
        Transform t_spawn = transform.Find("Cameras/Player Camera");

        if (Input.GetKeyDown(KeyCode.G)&& Input.GetKeyDown(KeyCode.H))
        {
            if (Physics.Raycast(t_spawn.position, t_spawn.forward, out RaycastHit raycastHit))
            {

                //Instantiate(bigcube, raycastHit.point, Quaternion.identity);
                hookshotPosition = raycastHit.point;
                thrownState = true;
            }
        }
    }











}
