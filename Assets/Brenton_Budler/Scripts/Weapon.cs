using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    #region Varaibles 
    public Gun[] loadout;
    public Transform weaponParent;
    public GameObject bulletHolePrefab;
    public LayerMask canBeShot;

    private float currentCooldown;
    public int currentIndex;
    public GameObject currentWeapon;

    public float warrior;

    //SOUND
    public AudioSource sfx;
    // public AudioClip hitmarkerSound;

    private Image hitmarkerImage;
    private float hitmarkerWait;

    private bool isReloading;
    public bool isAiming = false;


    public float dmgModifier; 
    #endregion





    #region Built-in Functions

    private void Start()
    {

             dmgModifier = 1;

           warrior = 1;
        foreach (Gun a in loadout)
        {
            a.Initialize();
        }

        Equip(0);

        hitmarkerImage = GameObject.Find("HUD/HitMarker/Image").GetComponent<Image>();
        hitmarkerImage.color = new Color(0, 1, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Equip(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Equip(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Equip(3);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Equip(4);
        }






        if (currentWeapon!=null)
        {
            Aim(Input.GetMouseButton(1));
            if (loadout[currentIndex].burst != 1)
            {
                if (loadout[currentIndex].burst!=1) {
                    if (Input.GetMouseButtonDown(0) && currentCooldown <= 0)
                    { 
                        if (loadout[currentIndex].FireBullet()) { Shoot(); }
                        else { StartCoroutine(Reload(loadout[currentIndex].reloadTime)); }
                    }
                }
            }
            else
            {
                if (Input.GetMouseButton(0) && currentCooldown <= 0)
                {
                    if (loadout[currentIndex].FireBullet()) { Shoot(); }
                    else { StartCoroutine(Reload(loadout[currentIndex].reloadTime)); }
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload(loadout[currentIndex].reloadTime));
            }

            //weapon position elasticity 
            currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime*4f);

            //cooldown for weapon
            if (currentCooldown>0){ currentCooldown -= Time.deltaTime; }


        }

        if (hitmarkerWait>0)
        {
            hitmarkerWait -= Time.deltaTime;
        }
        else if (hitmarkerImage.color.a > 0)
        {
            hitmarkerImage.color = Color.Lerp(hitmarkerImage.color, new Color(0, 1, 0, 0), Time.deltaTime*0.5f);
        }

        
    }

    #endregion

    #region Own Methods

    IEnumerator Reload(float p_wait)
    {
        isReloading = true;
        currentWeapon.SetActive(false);

        yield return new WaitForSeconds(p_wait);

        loadout[currentIndex].Reload();
        currentWeapon.SetActive(true);
        isReloading = false;
    }


    public void Equip(int p_ind)
    {

        if (currentWeapon != null){

            if (isReloading)
            {
                StopCoroutine("Reload");
            }
            Destroy(currentWeapon);

        }

        currentIndex = p_ind;

        GameObject t_newEquipment = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        t_newEquipment.transform.localPosition = Vector3.zero;
        t_newEquipment.transform.localEulerAngles = Vector3.zero;

        currentWeapon = t_newEquipment;
    }

    public void Drop()
    {

        //GameObject droppedWeapon = Instantiate(currentWeapon, transform.position, Quaternion.identity) as GameObject;
        //Destroy(currentWeapon);
        //droppedWeapon.transform.localPosition = Vector3.Lerp(droppedWeapon.transform.localPosition, weaponParent.forward*100f, Time.deltaTime * 0.25f);

    }

    void Aim(bool p_isAiming)
    {
        isAiming = p_isAiming;
        Transform t_anchor = currentWeapon.transform.Find("Anchor");
        Transform t_state_ads = currentWeapon.transform.Find("States/ADS");
        Transform t_state_hip = currentWeapon.transform.Find("States/Hip");

        if (p_isAiming)
        {
            //Aim
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
        }
        else
        {
            //Aim
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
        }


    }

    void Shoot()
    {
        Transform t_spawn = transform.Find("Cameras/Player Camera");

        //cooldown
        currentCooldown = loadout[currentIndex].firerate / warrior;

        for (int i = 0; i < Mathf.Max(1,loadout[currentIndex].pellets); i++)
        {
            //bloom (ACUURACY)
            Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
            t_bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.up;
            t_bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.right;
            t_bloom -= t_spawn.position;
            t_bloom.Normalize();

            //raycast
            RaycastHit t_hit = new RaycastHit();
            if (Physics.Raycast(t_spawn.position, t_bloom, out t_hit, 1000f, canBeShot))
            {
                GameObject t_newHole = Instantiate(bulletHolePrefab, t_hit.point + t_hit.normal * 0.0001f, Quaternion.identity) as GameObject;
                t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                Destroy(t_newHole, 5f);

                if (loadout[currentIndex].isProjectile)
                {

                    Transform shootPoint = currentWeapon.transform.Find("ShootPoint");
                    
                    GameObject currentBullet = Instantiate(loadout[currentIndex].projectilePrefab, shootPoint.position, Quaternion.identity);
                    currentBullet.transform.forward = t_bloom;
                    currentBullet.GetComponent<Rigidbody>().AddForce(t_bloom * loadout[currentIndex].projectileForce, ForceMode.Impulse);   
                }

                if (t_hit.collider.gameObject.layer == 12)
                {
                    t_hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(loadout[currentIndex].damage * dmgModifier);
                    hitmarkerImage.color = new Color(0, 1, 0, 1);
                    // sfx.PlayOneShot(hitmarkerSound);
                    hitmarkerWait = 0.5f;
                }
            }
            else
            {

                if (loadout[currentIndex].isProjectile)
                {

                    Transform shootPoint = currentWeapon.transform.Find("ShootPoint");

                    GameObject currentBullet = Instantiate(loadout[currentIndex].projectilePrefab, shootPoint.position, Quaternion.identity);
                    currentBullet.transform.forward = t_bloom;
                    currentBullet.GetComponent<Rigidbody>().AddForce(t_bloom * loadout[currentIndex].projectileForce, ForceMode.Impulse);
                    Debug.Log(this.GetComponent<Rigidbody>().velocity.y);

                }
            }
        }
        //SOUND
        //sfx.Stop();
        sfx.clip = loadout[currentIndex].gunshotSound;
        sfx.pitch = 1 - loadout[currentIndex].pitchRandomization + Random.Range(-loadout[currentIndex].pitchRandomization, loadout[currentIndex].pitchRandomization);
        sfx.Play();


        //gun fx 
        
        currentWeapon.transform.Rotate(-loadout[currentIndex].recoil, 0, 0);
        currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].kickback;




    }

    public void RefreshAmmo(Text p_text)
    {
        int t_clip = loadout[currentIndex].GetClip();
        int t_stache = loadout[currentIndex].GetStash();

        p_text.text = t_clip.ToString("D2") + "/ " + t_stache.ToString("D2");
    }


    //private void TakeDamage(p_damage)
    //{
    //    GetComponent<ENEMY>.TakeDamage(p_damage);
    //}

    public void unEquip()
    {
        if (currentWeapon != null)
        {

            if (isReloading)
            {
                StopCoroutine("Reload");
            }
            Destroy(currentWeapon);

        }
    }
    #endregion
    
}
