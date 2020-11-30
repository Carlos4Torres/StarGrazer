using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;

public class StarGrazerRotation : MonoBehaviour
{
    public enum TriggerDirection
    {
        FULL,
        HORIZONTAL,
        VERTICAL,
    }

    [Header("Rotate Values")]
    public float rotateValue;
    public float rotateSpeed;
    public float returnSpeed;

    public static bool changed = false;

    [Header("Direction")]
    public TriggerDirection direction;

    public Image crosshair;

    //private float rotationX = 0;
    //private float rotationY = 0;
    private Camera cam;
    private StarGrazerMovement movement;

    PlayerControls controls;
    Vector2 move;
    Vector3 point;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.PlayerMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.PlayerMove.canceled += ctx => move = Vector2.zero;

        cam = Camera.main;
        movement = GetComponentInParent<StarGrazerMovement>();
        //point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    void FixedUpdate()
    {
        if (changed)
            StartCoroutine(SlowDownRotation());

        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector3 worldPosCross = cam.ScreenToWorldPoint(crosshair.transform.position);
        Vector3 screenPosCross = crosshair.rectTransform.position;
        float angle = Mathf.Atan2(screenPosCross.y - screenPos.y, screenPosCross.x - screenPos.x) * 180 / Mathf.PI;

        switch (movement.state)
        {
            case StarGrazerMovement.movementState.FULL:
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(screenPosCross);
                Quaternion toAngle;

                if (Physics.Raycast(ray, out hit) && hit.transform.tag != "Player")
                {
                    //print(hit.point);
                    Vector3 dir = (hit.point - transform.position);
                    toAngle = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, toAngle, Time.deltaTime * rotateSpeed);
                }
                break;

            case StarGrazerMovement.movementState.HORIZONTAL:
                toAngle = Quaternion.Euler(-angle, 0, 0);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, toAngle, Time.deltaTime * rotateSpeed);
                break;

            case StarGrazerMovement.movementState.VERTICAL:
                toAngle = Quaternion.Euler(0, -angle + 90, 0);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, toAngle, Time.deltaTime * rotateSpeed);
                break;
        }
        //print(angle);
        //Rotater();
    }

    IEnumerator SlowDownRotation()
    {
        rotateSpeed = .85f;
        yield return new WaitForSeconds(3);
        rotateSpeed = 50;
        changed = false;
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
