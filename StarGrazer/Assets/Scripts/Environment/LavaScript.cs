using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Cinemachine;
using System;

public class LavaScript : MonoBehaviour
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
    public CinemachineDollyCart lavaDolly;
    public PlayerStatus player = PlayerStatus.APPROACHING;

    private float mainDollySpeed;

    private float moveDampen = 0;
    public float delay;
    public float delay2;

    void Start()
    {
        mainDollySpeed = mainDolly.m_Speed;
    }

    void Update()
    {
        switch (player)
        {
            case PlayerStatus.SLOWING:
                mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, 0, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                if (mainDolly.m_Speed - 2 < 0)
                {
                    mainDolly.m_Speed = 0;
                    player = PlayerStatus.STOPPED;
                    StartCoroutine(PlayerDelay(player));
                }
                break;

            case PlayerStatus.STOPPED:
                lavaDolly.m_Speed = Mathf.Lerp(lavaDolly.m_Speed, 35, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                break;

            case PlayerStatus.SPEEDING:
                mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, mainDollySpeed, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                if (mainDollySpeed - mainDolly.m_Speed < 1)
                {
                    mainDolly.m_Speed = mainDollySpeed;
                    moveDampen = 0;
                    StartCoroutine(PlayerDelay(player));
                }
                break;
            case PlayerStatus.GONE:
                lavaDolly.m_Speed = Mathf.Lerp(lavaDolly.m_Speed, mainDollySpeed, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.enabled = false;

            player = PlayerStatus.SLOWING;
        }
    }

    private IEnumerator PlayerDelay(PlayerStatus status)
    {
        if(status == PlayerStatus.STOPPED)
        {
            yield return new WaitForSeconds(delay);
            player = PlayerStatus.SPEEDING;
        }
        else if (status == PlayerStatus.SPEEDING)
        {
            yield return new WaitForSeconds(delay2);
            player = PlayerStatus.GONE;
        }

    }
}
