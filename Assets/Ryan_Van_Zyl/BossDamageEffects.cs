using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageEffects : MonoBehaviour
{

    private BossController bController;
    public GameObject sEffect1, sEffect2, sEffect3, sEffect4;
    public GameObject explode1, explode2, explode3, explode4;
    public GameObject body1, body2, body3, body4;
    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        bController = this.GetComponent<BossController>();
        maxHealth = bController.health;

        sEffect1.SetActive(false);
        sEffect2.SetActive(false);
        sEffect3.SetActive(false);
        sEffect4.SetActive(false);

        explode1.SetActive(false);
        explode2.SetActive(false);
        explode3.SetActive(false);
        explode4.SetActive(false);

        body1.SetActive(false);
        body2.SetActive(false);
        body3.SetActive(false);
        body4.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (bController.health <= (maxHealth/1.25))
        {
            sEffect1.SetActive(true);
        }
        if (bController.health <= (maxHealth/1.6))
        {
            sEffect2.SetActive(true);
        }
        if (bController.health <= (maxHealth/2.3))
        {
            sEffect3.SetActive(true);
        }
        if (bController.health <= (maxHealth/3.3))
        {
            sEffect4.SetActive(true);
        }

        if(bController.health <= 0)
        {
            explode1.SetActive(true);
            explode2.SetActive(true);
            explode3.SetActive(true);
            explode4.SetActive(true);

            body1.SetActive(true);
            body2.SetActive(true);
            body3.SetActive(true);
            body4.SetActive(true);
        }
    }
}
