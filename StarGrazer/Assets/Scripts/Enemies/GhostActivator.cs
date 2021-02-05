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

    void Update()
    {
        if(active)
        {
            ghostDollyScript.m_Speed = Mathf.Lerp(ghostDollyScript.m_Speed, 35, moveDampen);
            moveDampen += 0.007f * Time.deltaTime;
        }
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            active = true;
        }
    }
}
