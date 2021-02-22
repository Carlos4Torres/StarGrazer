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

    public int state = 0;

    private float moveDampen = 0;
    public GameObject lavaModelV;
    public GameObject lavaModelH;
    private float speed = 15;


    //public float delay;
    private void Start()
    {

    }

    void Update()
    {
       switch(state)
        {   
            case 1:
                lavaDollyH.m_Speed = Mathf.Lerp(lavaDollyH.m_Speed, 25, moveDampen);
                lavaDollyV.m_Speed = Mathf.Lerp(lavaDollyV.m_Speed, 35, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;

                break;
            case 2:
                lavaDollyV.m_Speed = Mathf.Lerp(lavaDollyV.m_Speed, 20, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                break;
        }

        lavaModelV.transform.localRotation *= Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.up);
        lavaModelH.transform.localRotation *= Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.up);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.enabled = false;

            state = 1;
            StartCoroutine(LavaSpeedDelay());

        }
    }

    private IEnumerator LavaSpeedDelay()
    {
        yield return new  WaitForSeconds(13.95f);
        moveDampen = 0;
        state = 2;
    }
}
