using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public enum TriggerDirection
    {
        FULL,
        HORIZONTAL,
        VERTICAL,
    }

    [Header("Player")]
    public StarGrazerMovement player;

    [Header("Direction")]
    public TriggerDirection direction;
    public float speed;

    [Header("Camera Information")]
    public Camera mainCamera;
    public Transform cameraLocation;
    private bool triggered;


    private void Update()
    {
        if (triggered)
        {
            // Vector3 Lerp and slerp are for smooth movement and rotations
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraLocation.position, speed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, cameraLocation.transform.rotation, speed * Time.deltaTime);
            
            if (Vector3.Distance(mainCamera.transform.position, cameraLocation.position) < 0.1f)
                triggered = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (direction)
            {
                case TriggerDirection.FULL:
                    player.state = StarGrazerMovement.movementState.FULL;
                    break;
                
                case TriggerDirection.HORIZONTAL:
                    player.state = StarGrazerMovement.movementState.HORIZONTAL;
                    break;

                case TriggerDirection.VERTICAL:
                    player.state = StarGrazerMovement.movementState.VERTICAL;
                    break;
            }

            triggered = true;
        }
    }

}
