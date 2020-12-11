using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Diagnostics;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] int thisIndex;
    [SerializeField] public static int menuScreen;
    [SerializeField] public static int clickControl;

    public void Start()
    {
        menuScreen = 1;
        clickControl = 0;
    }


    public void Update()
    {
        if (menuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("pressed", true);
                if (menuScreen == 1) //Main Menu
                {
                    if (menuButtonController.index == 1 && thisIndex == 1)
                    {
                        StartCoroutine(MenuStartGame());
                    }
                    if (menuButtonController.index == 2 && thisIndex == 2)
                    {
                        StartCoroutine(MenuMainToOptions());
                    }
                    if (menuButtonController.index == 3 && thisIndex == 3)
                    {
                        StartCoroutine(MenuMainToHighScores());
                    }
                    if (menuButtonController.index == 4 && thisIndex == 4)
                    {
                        StartCoroutine(MenuQuit());
                    }
                }
                if (menuScreen == 2) //Start Game Menu
                {
                    if (menuButtonController.index == 5 && thisIndex == 5)
                    {
                        StartCoroutine(MenuCasualMode());
                    }
                    if (menuButtonController.index == 6 && thisIndex == 6)
                    {
                        StartCoroutine(MenuVeteranMode());
                    }
                    if (menuButtonController.index == 7 && thisIndex == 7)
                    {
                        StartCoroutine(MenuTraining());
                    }
                    if (menuButtonController.index == 8 && thisIndex == 8)
                    {
                        StartCoroutine(MenuStartGameToMain());
                    }
                }
                if (menuScreen == 3) // Options Menu
                {
                    if (menuButtonController.index == 9 && thisIndex == 9)
                    {
                        StartCoroutine(MenuOptionsToMain());
                    }
                }
                if (menuScreen == 4) // HighScores Menu
                {
                    if (menuButtonController.index == 10 && thisIndex == 10)
                    {
                        StartCoroutine(MenuHighScoresToMain());
                    }
                }
            }
            else if (animator.GetBool("pressed") && clickControl == 0)
            {
                animator.SetBool("pressed", false);
            }
        }
        else if (clickControl == 0)
        {
            animator.SetBool("selected", false);
        }
    }

    public IEnumerator MenuStartGame()
    {
        clickControl = 1;
        menuScreen = 2;
        menuButtonController.index = 5;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuCasualMode()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level One");
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuVeteranMode()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("(WB) Level Two");
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuStartGameToMain()
    {
        clickControl = 1;
        menuScreen = 1;
        menuButtonController.index = 1;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuTraining()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level One");
        animator.SetBool("selected", false);
        clickControl = 0;
    }
    
    public IEnumerator MenuMainToOptions()
    {
        clickControl = 1;
        menuScreen = 3;
        menuButtonController.index = 9;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuOptionsToMain()
    {
        clickControl = 1;
        menuScreen = 1;
        menuButtonController.index = 1;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuMainToHighScores()
    {
        clickControl = 1;
        menuScreen = 4;
        menuButtonController.index = 10;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuHighScoresToMain()
    {
        clickControl = 1;
        menuScreen = 1;
        menuButtonController.index = 1;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuQuit()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    public void button_click()
    {
        if (menuScreen == 1)
        {
            if (thisIndex == 1)
            {
                StartCoroutine(MenuStartGame());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 2)
            {
                StartCoroutine(MenuMainToOptions());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 3)
            {
                StartCoroutine(MenuMainToHighScores());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 4)
            {
                StartCoroutine(MenuQuit());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
        }
        if (menuScreen == 2)
        {
            if (thisIndex == 5)
            {
                StartCoroutine(MenuCasualMode());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 6)
            {
                StartCoroutine(MenuVeteranMode());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 7)
            {
                StartCoroutine(MenuTraining());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 8)
            {
                StartCoroutine(MenuStartGameToMain());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
        }
        if (menuScreen == 3)
        {
            if (thisIndex == 9)
            {
                StartCoroutine(MenuOptionsToMain());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
        }
        if (menuScreen == 4)
        {
            if (thisIndex == 10)
            {
                StartCoroutine(MenuHighScoresToMain());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
        }
    }
}