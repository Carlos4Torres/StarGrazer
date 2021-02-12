using Cinemachine;
using System;
using UnityEngine;

public class SpeedTrigger : MonoBehaviour
{
    public CinemachineDollyCart mainDolly;
    private const int defaultSPEED = 20;


    private bool active = false;
    public SpeedChange changeType;

    public int newSpeed;

    private float moveDampen = 0;

    public enum SpeedChange
    {
        SLOWING,
        SPEEDING,
    }

    void Update()
    {
        if(active)
        {
            switch (changeType)
            {
                case SpeedChange.SLOWING:
                        mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, defaultSPEED, moveDampen);
                        moveDampen += 0.007f * Time.deltaTime;
                    if( mainDolly.m_Speed - defaultSPEED < 1)
                    {
                        mainDolly.m_Speed = defaultSPEED;
                        Destroy(this.gameObject);
                    }
                    break;
                case SpeedChange.SPEEDING:
                        mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, newSpeed, moveDampen);
                        moveDampen += 0.007f * Time.deltaTime;
                    if (newSpeed - mainDolly.m_Speed < 1)
                    {
                        mainDolly.m_Speed = newSpeed;
                        Destroy(this.gameObject);
                    }
                    break;
                default:
                    break;
            }
        }
     
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("PlayerHealth"))
        {
            active = true;
        }
    }

}
