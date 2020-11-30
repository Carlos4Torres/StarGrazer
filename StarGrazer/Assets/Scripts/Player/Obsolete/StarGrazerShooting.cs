using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StarGrazerMovement;

public class StarGrazerShooting : MonoBehaviour
{
    public StarGrazerMovement movement;
    public GameObject marker;

    PlayerControls controls;
    Vector3 move;

    private Camera cam;

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Start() => cam = Camera.main; // Creating a reference for Camera.main as it is faster than calling the camera directly, good to put into practice

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.ReticleMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.ReticleMove.canceled += ctx => move = Vector2.zero;
    }

    private void Update() // Update function to change the marker's position when the camera angle changes. 
    {
        Vector3 mousePos = new Vector3(move.x, move.y, 0); //Input.mousePosition;
        //Vector3 mousePos = new Vector3(move.x, move.y, move.z);

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
        marker.transform.position = mousePos;
    }
}
