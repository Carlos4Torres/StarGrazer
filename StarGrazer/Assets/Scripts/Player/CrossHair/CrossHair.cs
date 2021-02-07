using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrossHair : MonoBehaviour
{
    //public bool mouseAndKeyboard;
    [Range(0.1f, 10.0f)]
    public float controllerSensitivity;

    private bool controller;

    PlayerControls controls;
    Vector3 move;
    Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);

    //Next 3 functions are for input controls
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.ReticleMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.ReticleMove.canceled += ctx => move = Vector2.zero;
    }
    

    private void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Toggle mouse/controller control using M key
        if (Input.GetKeyDown(KeyCode.M) && controller)
            controller = false;
        else if (Input.GetKeyDown(KeyCode.M) && !controller)
            controller = true;

        //if Escape Key Press, Cursor will show
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        //if the mouse is on screen and mouse support is on, track crosshair to mouse
        if (screenRect.Contains(Input.mousePosition) && !controller)
        {
            transform.position = Input.mousePosition;
        }

        //if controller input is active, use input controls
        if (controller)
            transform.position += move * controllerSensitivity;
        else
            transform.position = Input.mousePosition;
    }
}
