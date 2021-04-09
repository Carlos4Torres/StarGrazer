using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public Vector3 rotation;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localEulerAngles = rotation;
    }
}
