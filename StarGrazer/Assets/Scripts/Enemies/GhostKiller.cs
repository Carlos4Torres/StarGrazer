using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKiller : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ghost"))
        {
            Destroy(other.gameObject);
        }
    }
}
