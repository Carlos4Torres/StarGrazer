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

    PlayerControls controls;
    Vector2 move;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.PlayerMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.PlayerMove.canceled += ctx => move = Vector2.zero;
    }

    void Update()
    {
        Rotater();
    }

    void Rotater()
    {
        rotationX += move.x * rotateSpeed * Time.deltaTime; /*Input.GetAxis("Horizontal")*/
        rotationY +=  move.y * rotateSpeed * Time.deltaTime; /*Input.GetAxis("Vertical")*/

        rotationX = Mathf.Clamp(rotationX, -rotateValue, rotateValue);
        rotationY = Mathf.Clamp(rotationY, -rotateValue, rotateValue);
        
        if (Mathf.Approximately(move.x, 0)) /*Input.GetAxis("Horizontal")*/
        {
            if (rotationX < 0) rotationX += returnSpeed;
            if (rotationX > 0) rotationX -= returnSpeed;
        }
        if (Mathf.Approximately(move.y, 0)) /*Input.GetAxis("Vertical")*/
        {
            if (rotationY < 0) rotationY += returnSpeed;
            if (rotationY > 0) rotationY -= returnSpeed;
        }

        Vector3 rotationValues = new Vector3(rotationY, transform.localRotation.eulerAngles.y, rotationX);
        transform.localEulerAngles = rotationValues;
    }
}
