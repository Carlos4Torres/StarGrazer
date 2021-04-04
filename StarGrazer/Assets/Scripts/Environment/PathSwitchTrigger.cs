using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSwitchTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineDollyCart mainDollyScipt;
    public CinemachineSmoothPath newPath;

    public float insertPoint = 0;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            mainDollyScipt.m_Path = newPath;

            mainDollyScipt.m_Position = insertPoint;

            //Destroy(this.gameObject);
        }
    }
}

