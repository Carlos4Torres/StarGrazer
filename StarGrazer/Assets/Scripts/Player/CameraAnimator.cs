using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraAnimator : MonoBehaviour
{
    public enum TriggerDirection
    {
        FULL,
        HORIZONTAL,
        VERTICAL,
    }

    [Header("Player")]
    public StarGrazerMovement player;

    [Header("Direction")]
    public TriggerDirection direction;
    public float speed;

    [Header("Camera Information")]
    public Camera mainCamera;

    private Animator animator;

    private void Awake()
    {
        animator = mainCamera.GetComponent<Animator>();

        switch(SceneManager.GetActiveScene().name)
        {
            case "(WB) Level One":
                animator.SetBool("HORIZONTAL", true);
                break;
            case "Level One":
                animator.SetBool("HORIZONTAL", true);
                break;
            case "Level One (Updated Visuals)":
                animator.SetBool("HORIZONTAL", true);
                break;
            case "(WB) Level Two":
                animator.SetBool("FULL", true);
                break;
            case "Level Two":
                animator.SetBool("FULL", true);
                break;
            case "(WB) Level 3":
                animator.SetBool("VERTICAL", true);
                break;
            case "Level 3":
                animator.SetBool("VERTICAL", true);
                break;
            case "(WB) Level 4":
                animator.SetBool("HORIZONTAL", true);
                break;
            case "Level 4":
                animator.SetBool("HORIZONTAL", true);
                break;
            case "(WB) Level 5":
                animator.SetBool("VERTICAL", true);
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StarGrazerRotation.changed = true;
            player.resetting = true;
            StartCoroutine(player.TurnOffResetting());
            switch (direction)
            {
                case TriggerDirection.FULL:
                    player.state = StarGrazerMovement.movementState.FULL;
                    if (animator.GetBool("VERTICAL"))
                    {
                        animator.SetBool("FULL", true);
                        animator.SetInteger("state", 2);
                        StartCoroutine(TurnOffLast("VERTICAL"));
                    }
                    else if (animator.GetBool("HORIZONTAL"))
                    {
                        animator.SetBool("FULL", true);
                        animator.SetInteger("state", 5);
                        StartCoroutine(TurnOffLast("HORIZONTAL"));
                    }
                    break;
                
                case TriggerDirection.HORIZONTAL:
                    player.state = StarGrazerMovement.movementState.HORIZONTAL;
                    if (animator.GetBool("FULL"))
                    {
                        animator.SetBool("HORIZONTAL", true);
                        animator.SetInteger("state", 3);
                        StartCoroutine(TurnOffLast("FULL"));
                    }
                    else if (animator.GetBool("VERTICAL"))
                    {
                        animator.SetBool("HORIZONTAL", true);
                        animator.SetInteger("state", 4);
                        StartCoroutine(TurnOffLast("VERTICAL"));
                    }
                    break;

                case TriggerDirection.VERTICAL:
                    player.state = StarGrazerMovement.movementState.VERTICAL;
                    if (animator.GetBool("HORIZONTAL"))
                    {
                        animator.SetBool("VERTICAL", true);
                        animator.SetInteger("state", 1);
                        StartCoroutine(TurnOffLast("HORIZONTAL"));
                    }
                    break;
            }
        }
    }

    IEnumerator TurnOffLast(string state)
    {
        yield return new WaitForSeconds(3);
        animator.SetBool(state, false);
    }
}
