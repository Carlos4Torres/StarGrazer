using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrossHair : MonoBehaviour
{
    //public bool mouseAndKeyboard;
    [Range(0.1f, 10.0f)]
    public float controllerSensitivity;

    PlayerControls controls;
    Vector3 move;

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
        //if Escape Key Press, Cursor will show
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
            transform.position += move * controllerSensitivity;
    }
}
