using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int menuScreen;

    void Update()
    {
        menuScreen = MenuButton.menuScreen;
        RenderSettings.skybox.SetFloat("_Rotation", Time.time);

        if (menuScreen == 1)
        {
            animator.SetInteger("menu", 1);
            animator.SetInteger("Direction", 1);
        }
        if (menuScreen == 2)
        {
            animator.SetInteger("menu", 2);
        }
        if (menuScreen == 3)
        {
            animator.SetInteger("menu", 3);
        }
        if (menuScreen == 4)
        {
            animator.SetInteger("menu", 4);
        }
        if (menuScreen == 5)
        {
            animator.SetInteger("menu", 5);
            animator.SetInteger("Direction", 2);
        }
        if (menuScreen == 6)
        {
            animator.SetInteger("menu", 6);
        }
    }
}
