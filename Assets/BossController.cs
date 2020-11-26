using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public Animator anim;
    public GameObject flyingEnemy;
    private float timer=0;
    private float timeSinceSpawn = 0;
    public float timeBetweenSpawnEvents=4;
    public float timeBetweenSpawns = 1;
    public float timeBetweenShots = 2;
    private float timeSinceShot = 0;
    public int amountToSpawn=1;
    public ProjectileController projCon;
    private int currentAmount;
    private float maxHealth = 500;
    public float health=500;
    private GameObject player;
    private Transform ui_BossHealthBar;
    public GameObject WinScreen;
    public Text killsText;

    // Start is called before the first frame update
    void Start()
    {
        WinScreen.SetActive(false);
        currentAmount = amountToSpawn;
        ui_BossHealthBar = GameObject.Find("BossEnemyPosition/Canvas/Health/Bar").transform;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player(Clone)");
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn > timeBetweenSpawnEvents)
        {
            anim.SetBool("open",true);
            timer += Time.deltaTime;
            if (timer>timeBetweenSpawns&&currentAmount>0)
            {
                currentAmount--;
                timer = 0;
                Instantiate(flyingEnemy, transform.position+ new Vector3(0,2.8f,0), Quaternion.identity);
            }
            if (currentAmount == 0)
            {
                currentAmount = amountToSpawn;
                anim.SetBool("open", false);
                timeSinceSpawn = 0;
                timer = timeBetweenSpawns;
            }
        }
        else
        {
            timeSinceShot += Time.deltaTime;
            if (timeSinceShot > timeBetweenShots)
            {
                timeSinceShot = 0;
                Debug.Log("asdfasd");
                projCon.ShootProjectile();
               
            }
        }
        UpdateHealthBar();
    }


    public void TakeDamage(float amount)
    {

        if (amount > 0)
        {
            PopUpController popup = player.GetComponent<PopUpController>();
            popup.DamageDealt((int)amount, Vector3.Distance(player.transform.position, transform.position));
        }

        health -= amount;

        if (health <= 0)
        {

            health = 0;
            WinScreen.SetActive(true);
            killsText.text = "" + GameController.instance.kills;
            Time.timeScale = 0.1f;
            player.GetComponent<Player>().dead = true;
            Invoke("FreezeTime", 0.5f);

        }

        UpdateHealthBar();
    }

    public void FreezeTime()
    {
        Time.timeScale = 0;
        Look.cursorLocked = false;
    }


    void UpdateHealthBar()
    {
        
        float t_health_ratio = ((float)health / (float)maxHealth);
        Debug.Log(t_health_ratio);


        ui_BossHealthBar.localScale = Vector3.Lerp(ui_BossHealthBar.localScale, new Vector3(t_health_ratio, 1, 1), Time.deltaTime * 8f);
        //ui_BossHealthBar.localScale = new Vector3(t_health_ratio, 1, 1);



    }
}
