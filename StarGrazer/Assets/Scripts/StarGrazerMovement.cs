﻿using UnityEngine;

public class StarGrazerMovement : MonoBehaviour
{
    
    public enum movementState {
        FULL,
        HORIZONTAL,
        VERTICAL,
    }
   
    [Header("State")]
    public movementState state;
    public float speed = 5;

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float yMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        switch(state)
        {
            case movementState.FULL:
                transform.Translate(xMovement, yMovement, 0f);
                break;
            case movementState.HORIZONTAL:
                transform.Translate(0f, yMovement, xMovement);
                break;
            case movementState.VERTICAL:
                transform.Translate(xMovement, 0f, yMovement);
                break;
         
                
          // Was trying something out to fix issues regarding movement and the clamp. Might tinker with this in the future.
          // case movementState.RESET:
          //     transform.Translate(0f, 0f, 0f);
          //     break;
        }

        PlayerMovementClamping();
    }

    void PlayerMovementClamping()
    {
        Vector3 viewportCoords = Camera.main.WorldToViewportPoint(transform.position);
     
        switch (state)
        {
            case movementState.FULL:
                viewportCoords.x = Mathf.Clamp(viewportCoords.x, xMinFull, xMaxFull);
                viewportCoords.y = Mathf.Clamp(viewportCoords.y, yMinFull, yMaxFull);
                break;
            case movementState.HORIZONTAL:
                viewportCoords.x = Mathf.Clamp(viewportCoords.x, xMinHorz, xMaxHorz);
                viewportCoords.y = Mathf.Clamp(viewportCoords.y, yMinHorz, yMaxHorz);
                break;
            case movementState.VERTICAL:
                viewportCoords.x = Mathf.Clamp(viewportCoords.x, xMinVert, xMaxVert);
                viewportCoords.y = Mathf.Clamp(viewportCoords.y, yMinVert, yMaxVert);
                break;
        }

        transform.position = Camera.main.ViewportToWorldPoint(viewportCoords);
    }

    [Header("Full Clamp Values")]
    [Range(0.0f, 1f)] public float xMinFull;
    [Range(0.0f, 1f)] public float xMaxFull;
    [Range(0.0f, 1f)] public float yMinFull;
    [Range(0.0f, 1f)] public float yMaxFull;

    [Header("Vertical Clamp Values")]
    [Range(0.0f, 1f)] public float xMinVert;
    [Range(0.0f, 1f)] public float xMaxVert;
    [Range(0.0f, 1f)] public float yMinVert;
    [Range(0.0f, 1f)] public float yMaxVert;

    [Header("Horizontal Clamp Values")]
    [Range(0.0f, 1f)] public float xMinHorz;
    [Range(0.0f, 1f)] public float xMaxHorz;
    [Range(0.0f, 1f)] public float yMinHorz;
    [Range(0.0f, 1f)] public float yMaxHorz;

}
