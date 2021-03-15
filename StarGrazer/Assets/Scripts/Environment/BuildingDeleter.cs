using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDeleter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buildings;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(buildings);
        }
    }
}
