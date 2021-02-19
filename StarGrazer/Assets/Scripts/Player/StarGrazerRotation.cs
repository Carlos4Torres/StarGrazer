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

    public int currentScene;
    public Image crosshair;

    //private float rotationX = 0;
    //private float rotationY = 0;
    private Camera cam;
    private StarGrazerMovement movement;
    private bool rotationBack;

    PlayerControls controls;
    Vector2 move;
    Vector3 point;
    Quaternion toAngle;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.PlayerMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.PlayerMove.canceled += ctx => move = Vector2.zero;

        cam = Camera.main;
        movement = GetComponentInParent<StarGrazerMovement>();
    }

    void FixedUpdate()
    {
        if (changed)
            StartCoroutine(SlowDownRotation());

        // Lerps rotate speed back to 50 after transition is over
        if (rotationBack)
        {
            rotateSpeed = Mathf.Lerp(rotateSpeed, 50, Time.deltaTime * 1.25f);
            rotationBack = false;
        }

        // Calculates angle between StarGrazer and Crosshair
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector3 screenPosCross = crosshair.rectTransform.position;
        float angle = Mathf.Atan2(screenPosCross.y - screenPos.y, screenPosCross.x - screenPos.x) * 180 / Mathf.PI;

        switch (movement.state)
        {
            // If movement state is full, lerp to look direction from raycast
            case StarGrazerMovement.movementState.FULL:
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(screenPosCross);

                if (Physics.Raycast(ray, out hit) && hit.transform.tag != "Player")
                {
                    Vector3 dir = (hit.point - transform.position);
                    if (!changed)
                        toAngle = Quaternion.LookRotation(dir);
                    else
                        CheckScene();
                    transform.rotation = Quaternion.Lerp(transform.rotation, toAngle, Time.deltaTime * rotateSpeed);
                }
                break;

            // If movement state is horizontal, lerp to horizontal position and look at crosshair
            case StarGrazerMovement.movementState.HORIZONTAL:
                if (!changed)
                    toAngle = Quaternion.Euler(-angle, 0, 0);
                else
                    toAngle = Quaternion.LookRotation(Vector3.forward);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, toAngle, Time.deltaTime * rotateSpeed);
                break;

            // If movement state is vertical, lerp to vertical position and look at crosshair
            case StarGrazerMovement.movementState.VERTICAL:
                if (!changed)
                    toAngle = Quaternion.Euler(0, -angle + 90, 0);
                else
                    toAngle = Quaternion.LookRotation(Vector3.forward);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, toAngle, Time.deltaTime * rotateSpeed);
                break;
        }
    }

    // Slows rotation during transitions
    IEnumerator SlowDownRotation()
    {
        rotateSpeed = 1.5f;
        yield return new WaitForSeconds(3);
        //rotateSpeed = 50;
        changed = false;
        rotationBack = true;
    }

    void CheckScene()
    {
        switch(currentScene)
        {
            case 1:
                toAngle = Quaternion.LookRotation(-Vector3.right);
                break;
            default:
                toAngle = Quaternion.LookRotation(Vector3.right);
                break;
        }
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
