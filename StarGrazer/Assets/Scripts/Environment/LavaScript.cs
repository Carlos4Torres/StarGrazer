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

    public StarGrazerMovement grazer;

    public int state = 0;

    private float moveDampen = 0;
    public GameObject lavaModelV;
    public GameObject lavaModelH;
    private float speed = 15;
    private Vector3 coordinates;


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
                lavaDollyV.m_Speed = Mathf.Lerp(lavaDollyV.m_Speed, 30, moveDampen);
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
        coordinates = Camera.main.WorldToViewportPoint(grazer.transform.position);
        float x = coordinates.x;

        //Debug.Log(x);
        if (x >= .7)      yield return new WaitForSeconds(12.15f);
        else if (x >= .6) yield return new WaitForSeconds(12.55f);
        else if (x >= .5) yield return new WaitForSeconds(12.95f);
        else if (x >= .4) yield return new WaitForSeconds(13.35f);
        else if (x >= .3) yield return new WaitForSeconds(13.75f);
        else if (x >= .2) yield return new WaitForSeconds(14.05f);
        else yield return new WaitForSeconds(14.45f);

        moveDampen = 0;
        state = 2;
    }
}
