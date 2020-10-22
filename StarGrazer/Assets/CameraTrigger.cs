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

            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, cameraLocation.position, speed * Time.deltaTime);
            mainCamera.transform.localEulerAngles = cameraLocation.transform.localEulerAngles;

        }
    }

}
