using UnityEngine;
using System.Collections;

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

    private void Start()
    {
        animator = mainCamera.GetComponent<Animator>();
    }

    //private void FixedUpdate()
    //{
    //    if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
    //        player.GetComponentInChildren<StarGrazerRotation>().rotateValue = 5;
    //    else
    //        player.GetComponentInChildren<StarGrazerRotation>().rotateValue = 5;
    //}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StarGrazerRotation.changed = true;
            switch (direction)
            {
                case TriggerDirection.FULL:
                    player.state = StarGrazerMovement.movementState.FULL;
                    animator.SetInteger("state", 1);
                    break;
                
                case TriggerDirection.HORIZONTAL:
                    player.state = StarGrazerMovement.movementState.HORIZONTAL;
                    animator.SetInteger("state", 2);
                    break;

                case TriggerDirection.VERTICAL:
                    player.state = StarGrazerMovement.movementState.VERTICAL;
                    animator.SetInteger("state", 3);
                    break;
            }
        }
    }
}
