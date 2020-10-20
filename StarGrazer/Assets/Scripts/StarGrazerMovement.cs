using UnityEngine;

public class StarGrazerMovement : MonoBehaviour
{

    public enum movementState {
        FULL,
        HORIZONTAL,
        VERTICAL,
    }
    
    public movementState state;
    public float speed = 5;
    
    [Header("X Clamp Values")]
    public float xMin;
    public float xMax;

    [Header("Y Clamp Values")]
    public float yMin;
    public float yMax;

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
        }

        //  //I think there's a better way to do this based off of the screen size/camera render, but need to do some research. 
        //  //Also doesn't work with changing screen/camera views right now
        //  Vector3 clampedPosition = transform.localPosition;
        //  clampedPosition.x = Mathf.Clamp(clampedPosition.x, xMin, xMax);
        //  clampedPosition.y = Mathf.Clamp(clampedPosition.y, yMin, yMax);
        //
        //  transform.localPosition = clampedPosition;
    }

}
