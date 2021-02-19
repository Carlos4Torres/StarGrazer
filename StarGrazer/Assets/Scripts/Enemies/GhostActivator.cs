using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GhostActivator : MonoBehaviour
{
    public CinemachineDollyCart mainDollyScipt;
    public CinemachineDollyCart ghostDollyScript;
    private float moveDampen = 0;
    private bool active = false;

    private float delay = 2.5f;

    void Update()
    {
        if(active)
        {
            ghostDollyScript.m_Speed = Mathf.Lerp(ghostDollyScript.m_Speed, 35, moveDampen);
            moveDampen += 0.007f * Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Delay());
        }        
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        active = true;
    }    
}
