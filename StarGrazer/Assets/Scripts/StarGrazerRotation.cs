using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;

public class StarGrazerRotation : MonoBehaviour
{
    [Header("Rotate Values")]
    public float rotateValue;
    public float rotateSpeed;
    public float returnSpeed;
    public Image crosshair;

    //private float rotationX = 0;
    //private float rotationY = 0;
    private Camera cam;


    PlayerControls controls;
    Vector2 move;
    Vector3 point;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.PlayerMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.PlayerMove.canceled += ctx => move = Vector2.zero;
        cam = Camera.main;
        //point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    void FixedUpdate()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector3 screenPosCross = crosshair.rectTransform.position;
        float angle = Mathf.Atan2(screenPosCross.y - screenPos.y, screenPosCross.x - screenPos.x) * 180 / Mathf.PI;
        //if (angle < 0)
        //    angle += 360;
        Quaternion toAngle = Quaternion.Euler(angle, 180, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, toAngle, Time.deltaTime * rotateSpeed);
        print(angle);
        //Rotater();
    }

    //void Rotater()
    //{
    //    rotationX += move.x * rotateSpeed * Time.deltaTime; /*Input.GetAxis("Horizontal")*/
    //    rotationY +=  move.y * rotateSpeed * Time.deltaTime; /*Input.GetAxis("Vertical")*/

    //    rotationX = Mathf.Clamp(rotationX, -rotateValue, rotateValue);
    //    rotationY = Mathf.Clamp(rotationY, -rotateValue, rotateValue);
        
    //    if (Mathf.Approximately(move.x, 0)) /*Input.GetAxis("Horizontal")*/
    //    {
    //        if (rotationX < 0) rotationX += returnSpeed;
    //        if (rotationX > 0) rotationX -= returnSpeed;
    //    }
    //    if (Mathf.Approximately(move.y, 0)) /*Input.GetAxis("Vertical")*/
    //    {
    //        if (rotationY < 0) rotationY += returnSpeed;
    //        if (rotationY > 0) rotationY -= returnSpeed;
    //    }

    //    Vector3 rotationValues = new Vector3(rotationY, transform.localRotation.eulerAngles.y, rotationX);
    //    transform.localEulerAngles = rotationValues;
    //}
}
