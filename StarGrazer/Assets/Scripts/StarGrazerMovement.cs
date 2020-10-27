using UnityEngine;

public class StarGrazerMovement : MonoBehaviour
{
    
    public enum movementState {
        FULL,
        HORIZONTAL,
        VERTICAL,
    }
   
    [Header("State")]
    public movementState state;
    public float speed = 5;

    [Header("X Clamp Values")]
    [Range(0.0f, 1f)]
    public float xMin;
    [Range(0.0f, 1f)]
    public float xMax;

    [Header("Y Clamp Values")]
    [Range(0.0f, 1f)]
    public float yMin;
    [Range(0.0f, 1f)]
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
         
                
          // Was trying something out to fix issues regarding movement and the clamp. Might tinker with this in the future.
          // case movementState.RESET:
          //     transform.Translate(0f, 0f, 0f);
          //     break;
        }

        PlayerMovementClamping();
    }

    void PlayerMovementClamping()
    {
        Vector3 viewportCoords = Camera.main.WorldToViewportPoint(transform.position);
        
        viewportCoords.x = Mathf.Clamp(viewportCoords.x, xMin, xMax);
        viewportCoords.y = Mathf.Clamp(viewportCoords.y, yMin, yMax);
       
        transform.position = Camera.main.ViewportToWorldPoint(viewportCoords);
    }

}
