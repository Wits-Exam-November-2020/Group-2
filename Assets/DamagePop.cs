using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePop : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float disappearTimer;
    private float xline;
    private float yline;
    private Color textColor;

    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
        xline = Random.Range(-5000, 5000);
        yline = Random.Range(-5000, 5000);
        transform.position += new Vector3(xline/100, yline/100, 0);
        textColor = textMesh.color;
        disappearTimer = 0.5f;
    }
    private void Update()
    {
        Vector3 dir = new Vector3(xline, yline, 0);
        Vector3 finalDir = dir.normalized;

         float moveSpeed = 20f;
        transform.position += finalDir * moveSpeed * Time.deltaTime;

   

        disappearTimer -= Time.deltaTime;
        if (disappearTimer<0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a<0)
            {
                GameObject parent = transform.parent.transform.parent.gameObject;
                Destroy(parent);

            }
        }

    }
}
