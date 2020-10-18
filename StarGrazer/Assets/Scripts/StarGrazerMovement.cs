using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGrazerMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("X Clamp Values")]
    public float xMin;
    public float xMax;

    [Header("Y Clamp Values")]
    public float yMin;
    public float yMax;

    void Update()
    {
        Movement3D();
    }

    void Movement3D()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float yMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(xMovement, yMovement, 0f);
        Vector3 clampedPosition = transform.localPosition;

        //I think there's a better way to do this based off of the screen size/camera render, but need to do some research. 
        //It works for now, but will be messy in the 2d/3d changing portions
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, xMin, xMax);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, yMin, yMax);

        transform.localPosition = clampedPosition;
    }

}
