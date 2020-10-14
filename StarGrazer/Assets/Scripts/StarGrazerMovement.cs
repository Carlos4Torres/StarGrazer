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
        float yMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(xMovement, yMovement, 0f);
        Vector3 clampedPosition = transform.localPosition;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, xMin, xMax);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, yMin, yMax);

        transform.localPosition = clampedPosition;
    }
}
