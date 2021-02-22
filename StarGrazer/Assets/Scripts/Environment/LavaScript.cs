using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Cinemachine;
using System;

public class LavaScript : MonoBehaviour
{

    public CinemachineDollyCart lavaDollyV;
    public CinemachineDollyCart lavaDollyH;

    public bool stopper = false;
    public int state = 0;

    private float moveDampen = 0;
    //public float delay;

    void Update()
    {
       switch(state)
        {
            case 1:
                lavaDollyH.m_Speed = Mathf.Lerp(lavaDollyH.m_Speed, 30, moveDampen);
                lavaDollyV.m_Speed = Mathf.Lerp(lavaDollyV.m_Speed, 35, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                break;
            case 2:
                lavaDollyV.m_Speed = Mathf.Lerp(lavaDollyV.m_Speed, 20, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                break;
            case 3:
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(stopper == false)
            {
                BoxCollider collider = GetComponent<BoxCollider>();
                collider.enabled = false;

                state = 1;
                StartCoroutine(LavaSpeedDelay());
                
            }
            else if (stopper == true)
            {
                BoxCollider collider = GetComponent<BoxCollider>();
                collider.enabled = false;

                state = 3;
            }

        }
    }

    private IEnumerator LavaSpeedDelay()
    {
        yield return new  WaitForSeconds(12);
        moveDampen = 0;
        state = 2;
    }
}
