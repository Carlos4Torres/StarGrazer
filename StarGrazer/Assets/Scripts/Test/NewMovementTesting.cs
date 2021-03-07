using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementTesting : MonoBehaviour
{
    public List<Transform> points;
    public float rotateSpeed = .5f, forwardSpeed = 5f;

    private Transform currentPoint;
    private float distance;
    private int pointNum;
    private bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        pointNum = 1;
        currentPoint = points[pointNum];
    }

    // Update is called once per frame
    void Update()
    {
        if (!end)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * forwardSpeed);
            distance = Vector3.Distance(currentPoint.position, transform.position);

            if (distance < 1)
            {
                if (pointNum != points.Count - 1)
                    currentPoint = points[++pointNum];
                else
                {
                    //pointNum = 0;
                    //currentPoint = points[pointNum];
                    end = true;
                }
            }

            Vector3 dir = (transform.position - currentPoint.position);
            Quaternion toAngle = Quaternion.LookRotation(dir, Vector3.up);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, toAngle, Time.deltaTime * rotateSpeed);
        }
    }
}
