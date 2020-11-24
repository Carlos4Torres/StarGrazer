using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGrazerGrazing : MonoBehaviour
{
    [SerializeField]
    private int grazeCount; 

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("EnemyBullet"))
        {
            grazeCount++;
        }
    }
}
