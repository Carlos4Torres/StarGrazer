using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StarGrazerMovement;

public class StarGrazerShooting : MonoBehaviour
{
    public StarGrazerMovement movement;
    public GameObject marker;

    private Camera cam;

    private void Awake() => cam = Camera.main; // Creating a reference for Camer.main as it is faster than calling the camera directly, good to put into practice

    private void Update() // Update function to change the marker's position when the camera angle changes. 
    {
        Vector3 mousePos = Input.mousePosition;

        switch (movement.state)
        {
            case movementState.HORIZONTAL:
                mousePos.z = cam.transform.position.x;  
                break;

            case movementState.VERTICAL:
                mousePos.z = cam.transform.position.y;
                break;

            //case movementState.FULL:
            //    mousePos.z = cam.transform.position.z; // Still a WIP as does not function properly.
            //    break;
        }

        Vector3 worldMouse = cam.ScreenToWorldPoint(mousePos);
        marker.transform.position = worldMouse;
    }
}
