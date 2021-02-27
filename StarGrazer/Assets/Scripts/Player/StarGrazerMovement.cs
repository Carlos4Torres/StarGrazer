using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarGrazerMovement : MonoBehaviour
{
    
    public enum movementState {
        FULL,
        HORIZONTAL,
        VERTICAL,
    }

    PlayerControls controls;
    Vector2 move;

    [Header("State")]
    public movementState state;
    public float _SPEED = 20;
    public float speed;

    public bool resetting = false;

    public GameObject model;
    public SpriteRenderer focusingSprite;
    public AudioSource deathSound;
    //private bool focusing = false;
    public PlayerHealth playerHealth;

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.PlayerMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.PlayerMove.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Focus.started += ctx => Focus(true);
        controls.Gameplay.Focus.canceled += ctx => Focus(false);

        speed = _SPEED;
    }

    void Update()
    {
        Movement();
        //Need this bit to reset player position and actually have the enemy bullets hit
        if(resetting)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 3, 0), speed * Time.deltaTime);
        }
    }

    public IEnumerator TurnOffResetting()
    {
        yield return new WaitForSeconds(3);
        resetting = false;
    }

    void Movement()
    {
        float xMovement = move.x * speed * Time.unscaledDeltaTime; /*Input.GetAxis("Horizontal")*/
        float yMovement = move.y * speed * Time.unscaledDeltaTime; /*Input.GetAxis("Vertical")*/

        switch (state)
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

            switch (state)
            {
                case movementState.FULL: 

                    if(viewportCoords.x < xMinFull || viewportCoords.y < yMinFull || viewportCoords.x > xMaxFull || viewportCoords.y > yMaxFull)
                    {
                        viewportCoords.x = Mathf.Clamp(viewportCoords.x, xMinFull, xMaxFull);
                        viewportCoords.y = Mathf.Clamp(viewportCoords.y, yMinFull, yMaxFull);

                        transform.position = Camera.main.ViewportToWorldPoint(viewportCoords);
                    }

                    break;

                case movementState.HORIZONTAL:
                
                    if (viewportCoords.x < xMinHorz || viewportCoords.y < yMinHorz || viewportCoords.x > xMaxHorz || viewportCoords.y > yMaxHorz)
                    {
                        viewportCoords.x = Mathf.Clamp(viewportCoords.x, xMinHorz, xMaxHorz);
                        viewportCoords.y = Mathf.Clamp(viewportCoords.y, yMinHorz, yMaxHorz);

                        transform.position = Camera.main.ViewportToWorldPoint(viewportCoords);
                    }
                    
                    break;

                case movementState.VERTICAL:
                    if (viewportCoords.x < xMinVert || viewportCoords.y < yMinVert || viewportCoords.x > xMaxVert || viewportCoords.y > yMaxVert)
                    {
                        viewportCoords.x = Mathf.Clamp(viewportCoords.x, xMinVert, xMaxVert);
                        viewportCoords.y = Mathf.Clamp(viewportCoords.y, yMinVert, yMaxVert);

                        transform.position = Camera.main.ViewportToWorldPoint(viewportCoords);
                    }
                   
                    break;
            }
    }

    void Focus(bool focusing)
    {
        if (focusing)
        {
            speed = _SPEED / 2;
            focusingSprite.enabled = true;
        }
        else
        {   
            speed = _SPEED;
            focusingSprite.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Enemy Model")  || other.CompareTag("Ghost")) && !playerHealth.flickering && !playerHealth.respawning)
        {
            playerHealth.Damage(1);
            if (!playerHealth.respawning) StartCoroutine(playerHealth.Flicker());
        }
    }

    [Header("Full Clamp Values")]
    [Range(0.0f, 1f)] public float xMinFull;
    [Range(0.0f, 1f)] public float xMaxFull;
    [Range(0.0f, 1f)] public float yMinFull;
    [Range(0.0f, 1f)] public float yMaxFull;

    [Header("Vertical Clamp Values")]
    [Range(0.0f, 1f)] public float xMinVert;
    [Range(0.0f, 1f)] public float xMaxVert;
    [Range(0.0f, 1f)] public float yMinVert;
    [Range(0.0f, 1f)] public float yMaxVert;

    [Header("Horizontal Clamp Values")]
    [Range(0.0f, 1f)] public float xMinHorz;
    [Range(0.0f, 1f)] public float xMaxHorz;
    [Range(0.0f, 1f)] public float yMinHorz;
    [Range(0.0f, 1f)] public float yMaxHorz;


    //public void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.CompareTag("Enemy") || (collision.collider.CompareTag("Boss") || collision.collider.CompareTag("Enemy Model")))
    //    {
    //        StartCoroutine(playerHealth.Respawn());
    //    }
    //}

    // IEnumerator DestroyModel()
    // {
    //     deathSound.Play();
    //     Destroy(model); 
    //     yield return new WaitForSeconds(5);
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }

}
