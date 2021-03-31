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
    public int newShotSpeed;

    private float moveDampen = 0;
    private float defaultShotSpeed;
    public Weapon weaponScript;

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
                    weaponScript.shotSpeed = defaultShotSpeed / 6;
                    moveDampen += 0.007f * Time.deltaTime;

                    if( mainDolly.m_Speed - newSpeed < 1)
                    {
                        mainDolly.m_Speed = newSpeed;
                        weaponScript.shotSpeed = newShotSpeed;
                        moveDampen = 0;
                        if (DestroyMe) Destroy(this.gameObject);
                    }

                    break;

                case SpeedChange.SPEEDING:

                    if (newSpeed < 20)
                    {
                        mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, defaultSPEED, moveDampen);
                        weaponScript.shotSpeed = Mathf.Lerp(weaponScript.shotSpeed, defaultShotSpeed, moveDampen);
                        if (defaultSPEED - mainDolly.m_Speed < 1)
                        {
                            mainDolly.m_Speed = defaultSPEED;
                            weaponScript.shotSpeed = defaultShotSpeed;
                            moveDampen = 0;
                            if (DestroyMe) Destroy(this.gameObject); else active = false;
                        }
                    }
                    else
                    {
                        mainDolly.m_Speed = Mathf.Lerp(mainDolly.m_Speed, newSpeed, moveDampen);
                        weaponScript.shotSpeed = Mathf.Lerp(weaponScript.shotSpeed, newShotSpeed, moveDampen);

                        if (newSpeed - mainDolly.m_Speed < 1)
                        {
                            mainDolly.m_Speed = newSpeed;
                            weaponScript.shotSpeed = newShotSpeed;
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
            defaultShotSpeed = weaponScript.shotSpeed;
            if (timer != 0) StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {        
        yield return new WaitForSeconds(timer);
        changeType = SpeedChange.SPEEDING;
    }

}
