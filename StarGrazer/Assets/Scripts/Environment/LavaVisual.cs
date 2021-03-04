using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaVisual : MonoBehaviour
{

    enum MovementType
    {
        ONEWAY,
        PINGPONG
    }

    [Header("Movement")]
    [SerializeField]
    private bool movement;
    [SerializeField]
    private float moveSpeed = .05f;
    [SerializeField]
    private Vector3 newPos;
    [SerializeField]
    private MovementType moveType = MovementType.ONEWAY;

    private int pos = 1;
    private Vector3 startPos;


    [Header("Rotation")]
    [SerializeField]
    private bool rotation;
    [SerializeField]
    private float rotateSpeed = .05f;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (movement)
        {
            switch(moveType)
            {
                case MovementType.ONEWAY:
                    transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, moveSpeed * Time.deltaTime);
                    break;
                case MovementType.PINGPONG:
                    if (pos == 1)
                    {
                        transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, moveSpeed * Time.deltaTime);
                        if (Vector3.Distance(transform.localPosition, newPos) < 2.0f) pos = 0;
                    }
                    else
                    {
                        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, moveSpeed * Time.deltaTime);
                        if (Vector3.Distance(transform.localPosition, startPos) < 2.0f) pos = 1;
                    }

                    break;

            }
        }


        if(rotation)transform.localRotation *= Quaternion.AngleAxis(rotateSpeed * Time.deltaTime, Vector3.up);
    }
}
