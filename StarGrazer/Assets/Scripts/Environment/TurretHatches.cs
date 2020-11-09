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
        SPEEDING
    }

    public enum HatchStatus
    {
        SLOWING,
        STOPPED,
        SPEEDING
    }

    public CinemachineDollyCart mainDolly;
    public PlayerStatus player = PlayerStatus.APPROACHING;
    
    private float mainDollySpeed;
    public float stopPosition;

    private float moveDampen = 0;

    void Start()
    {
        mainDollySpeed = mainDolly.m_Speed;
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
              // mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, 0, moveDampen);
              // moveDampen += 0.007f * Time.deltaTime;
              // if (mainDolly.m_Speed - 1 < 0)
              // {
              //     mainDolly.m_Speed = 0;
              //     player = PlayerStatus.STOPPED;
              // }
                break;
            
            case PlayerStatus.SPEEDING:
                mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, 0, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                if (mainDolly.m_Speed - 1 < 0)
                {
                    mainDolly.m_Speed = 0;
                    player = PlayerStatus.STOPPED;
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
