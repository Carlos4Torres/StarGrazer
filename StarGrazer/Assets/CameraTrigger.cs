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

    public TriggerDirection direction;

    public float speed;
    public StarGrazerMovement player;

    public Camera mainCamera;
    public Transform location3D;
    public Transform location2DVert;
    public Transform location2DHor;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("that");
            switch (direction)
            {
                case TriggerDirection.FULL:
                    mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, location3D.position, speed * Time.deltaTime);
                    player.state = StarGrazerMovement.movementState.FULL;
                    mainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
                    break;
                case TriggerDirection.HORIZONTAL:
                    mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, location2DHor.position, speed * Time.deltaTime);
                    break;
                case TriggerDirection.VERTICAL:
                    mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, location2DVert.position, speed * Time.deltaTime);
                    break;
                default:
                    break;
            }

        }
    }

}
