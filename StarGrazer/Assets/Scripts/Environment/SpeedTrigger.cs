using Cinemachine;
using System;
using UnityEngine;
using System.Collections;

public class SpeedTrigger : MonoBehaviour
{
    public CinemachineDollyCart mainDolly;
    private const int defaultSPEED = 20;


    private bool active = false;
    private bool deactivate = false;
    public SpeedChange changeType;

    public float timer;

    public bool DestroyMe = true;

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
                       
                    mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, newSpeed, moveDampen);
                    moveDampen += 0.007f * Time.deltaTime;

                    if( mainDolly.m_Speed - newSpeed < 1)
                    {
                        mainDolly.m_Speed = newSpeed;
                        moveDampen = 0;
                        if (DestroyMe) Destroy(this.gameObject);
                    }

                    break;

                case SpeedChange.SPEEDING:

                    if (newSpeed < 20)
                    {
                        mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, defaultSPEED, moveDampen);
                        if (defaultSPEED - mainDolly.m_Speed < 1)
                        {
                            mainDolly.m_Speed = defaultSPEED;
                            moveDampen = 0;
                            if (DestroyMe) Destroy(this.gameObject); else active = false;
                        }
                    }
                    else
                    {
                        mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, newSpeed, moveDampen);

                        if (newSpeed - mainDolly.m_Speed < 1)
                        {
                            mainDolly.m_Speed = newSpeed;
                            moveDampen = 0;
                            if (DestroyMe) Destroy(this.gameObject); else active = false;
                        }
                    }

                  

                    moveDampen += 0.007f * Time.deltaTime;


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
            if (timer != 0) StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        
        yield return new WaitForSeconds(timer);
        changeType = SpeedChange.SPEEDING;
    }

}
