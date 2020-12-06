using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clusterbullet : MonoBehaviour
{
    public GameObject boss;
    public float speed;
    public bool on;


    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        on = false;

    }

    // Update is called once per frame
    void Update()
    {
        if ((boss.GetComponent<BossController>().burst == false) && (on == false)) { transform.Translate(0, 0, 0.18f); }

        if (boss.GetComponent<BossController>().burst == true) { on = true; }

        if (on )
        {
            if (transform.localScale.x <= 1) { transform.Translate(0, 0, -0.2f); }
            if ((transform.localScale.x > 1) && (transform.localScale.x <= 1.5)) { transform.Translate(0, 0, -0.1f); }
            if ((transform.localScale.x > 1.5) && (transform.localScale.x <= 2)) { transform.Translate(0, 0, -0.05f); }
            if ((transform.localScale.x > 2) && (transform.localScale.x <= 2.5)) { transform.Translate(0, 0, -0.025f); }
            if ((transform.localScale.x > 2.5) && (transform.localScale.x <= 3)) { transform.Translate(0, 0, -0.0125f); }
        }

    }
}
