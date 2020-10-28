﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairFollower : MonoBehaviour
{
    //public Transform playerShip;
    //public float lookDistance = 5;

    PlayerControls controls;
    Vector2 move;

    //private Plane plane;
    //private Vector3 distanceFromCam;

    //private void Start()
    //{
    //    distanceFromCam = playerShip.position + new Vector3(0, 0, 10);
    //    // Creates a flat plane in front of the player object
    //    plane = new Plane(playerShip.forward, distanceFromCam);
    //}

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.ReticleMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.ReticleMove.canceled += ctx => move = Vector2.zero;
    }

    private void Update()
    {
        FollowMouse();
        print(move);
    }

    private void FollowMouse()
    {
        Vector2 m = new Vector2 (move.x, move.y);
        transform.Translate(m, Space.Self);

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
