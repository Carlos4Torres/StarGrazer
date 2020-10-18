using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairFollower : MonoBehaviour
{
    public Transform playerShip;
    //public float lookDistance = 5;

    //private Plane plane;
    //private Vector3 distanceFromCam;

    //private void Start()
    //{
    //    distanceFromCam = playerShip.position + new Vector3(0, 0, 10);
    //    // Creates a flat plane in front of the player object
    //    plane = new Plane(playerShip.forward, distanceFromCam);
    //}

    private void Update()
    {
        FollowMouse();   
    }

    private void FollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        transform.position = mousePos;

        //Sends a ray to track where the cursor is on the plane and adjusts the player model to slightly tilt towards that direction
        //Ray ray = Camera.main.ScreenPointToRay(mousePos);
        //float distanceToPoint = 0.0f;
        //if (plane.Raycast(ray, out distanceToPoint))
        //{
        //    Vector3 hitPoint = ray.GetPoint(distanceToPoint);
        //    hitPoint = Vector3.ClampMagnitude(hitPoint, lookDistance);
        //    playerShip.LookAt(hitPoint);
        //}
    }
}
