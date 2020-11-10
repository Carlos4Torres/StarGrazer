using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TurretHatches : MonoBehaviour
{

    public enum PlayerStatus
    {
        APPROACHING,
        SLOWING,
        STOPPED,
        SPEEDING,
        GONE
    }

    public enum HatchStatus
    {
        SLOWING,
        STOPPED,
        SPEEDING,

    }

    public CinemachineDollyCart mainDolly;
    public PlayerStatus player = PlayerStatus.APPROACHING;
    
    private float mainDollySpeed;
    public float stopPosition;

    public GameObject upperHatch;
    public Transform endUpper;
    private Transform startUpper;
    
    public GameObject lowerHatch;
    public Transform endLower;
    private Transform startLower;

    private float moveDampen = 0;

    void Start()
    {
        mainDollySpeed = mainDolly.m_Speed;

        startUpper = upperHatch.transform;
        startLower = lowerHatch.transform;
    }

    void Update()
    {
        switch(player)
        {
            case PlayerStatus.SLOWING:
                mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, 0, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                if(mainDolly.m_Speed - 1 < 0)
                {
                    mainDolly.m_Speed = 0;
                    player = PlayerStatus.STOPPED;
                }
                break;
           
            case PlayerStatus.STOPPED:
                upperHatch.transform.position = Vector3.Lerp(startUpper.position, endUpper.position, Time.deltaTime);
                lowerHatch.transform.position = Vector3.Lerp(startLower.position, endLower.position, Time.deltaTime);

                if(lowerHatch.transform.position.y - endLower.position.y <1)
                {
                    player = PlayerStatus.SPEEDING;
                }
                break;
            
            case PlayerStatus.SPEEDING:
                mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, mainDollySpeed, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                if (mainDollySpeed - mainDolly.m_Speed < 1)
                {
                    mainDolly.m_Speed = mainDollySpeed;
                    player = PlayerStatus.GONE;
                }
                break;
        }    
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.enabled = false;

            player = PlayerStatus.SLOWING;
        }
    }
}
