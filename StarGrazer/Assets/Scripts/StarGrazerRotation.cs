using UnityEngine;
using UnityEngine.Assertions.Must;

public class StarGrazerRotation : MonoBehaviour
{
    [Header("Rotate Values")]
    public float rotateValue;
    public float rotateSpeed;
    public float returnSpeed;

    private float rotationX = 0;
    private float rotationY = 0;

    void Update()
    {
        Rotater();
    }

    void Rotater()
    {
        rotationX += Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        rotationY += Input.GetAxis("Vertical") * rotateSpeed * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -rotateValue, rotateValue);
        rotationY = Mathf.Clamp(rotationY, -rotateValue, rotateValue);
        
        if (Mathf.Approximately(Input.GetAxis("Horizontal"), 0))
        {
            if (rotationX < 0) rotationX += returnSpeed;
            if (rotationX > 0) rotationX -= returnSpeed;
        }
        if (Mathf.Approximately(Input.GetAxis("Vertical"), 0))
        {
            if (rotationY < 0) rotationY += returnSpeed;
            if (rotationY > 0) rotationY -= returnSpeed;
        }

        Vector3 rotationValues = new Vector3(rotationY, transform.localRotation.eulerAngles.y, rotationX);
        transform.localEulerAngles = rotationValues;
    }
}
