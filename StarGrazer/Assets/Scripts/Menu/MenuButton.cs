using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Hosting;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] int thisIndex;
    [SerializeField] public static int menuScreen;

    public void Start()
    {
        menuScreen = 1;
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
                        StartCoroutine(MenuTutorial());
                    }
                    if (menuButtonController.index == 3 && thisIndex == 3)
                    {
                        StartCoroutine(MenuMainToOptions());
                    }
                    if (menuButtonController.index == 4 && thisIndex == 4)
                    {
                        StartCoroutine(MenuMainToAchievements());
                    }
                    if (menuButtonController.index == 5 && thisIndex == 5)
                    {
                        StartCoroutine(MenuQuit());
                    }
                }
                if (menuScreen == 2) //Start Game Menu
                {
                    if (menuButtonController.index == 6 && thisIndex == 6)
                    {
                        StartCoroutine(MenuCasualMode());
                    }
                    if (menuButtonController.index == 7 && thisIndex == 7)
                    {
                        StartCoroutine(MenuVeteranMode());
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
                if (menuScreen == 4) // Achievements Menu
                {
                    if (menuButtonController.index == 10 && thisIndex == 10)
                    {
                        StartCoroutine(MenuAchievementsToMain());
                    }
                }
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }

    public IEnumerator MenuStartGame()
    {
        menuScreen = 2;
        menuButtonController.index = 6;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator MenuCasualMode()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level 1 whitebox");
    }

    public IEnumerator MenuVeteranMode()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level 1 whitebox");
    }

    public IEnumerator MenuStartGameToMain()
    {
        menuScreen = 1;
        menuButtonController.index = 1;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator MenuTutorial()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level 1 whitebox");
    }
    
    public IEnumerator MenuMainToOptions()
    {
        menuScreen = 3;
        menuButtonController.index = 9;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator MenuOptionsToMain()
    {
        menuScreen = 1;
        menuButtonController.index = 1;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator MenuMainToAchievements()
    {
        menuScreen = 4;
        menuButtonController.index = 10;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator MenuAchievementsToMain()
    {
        menuScreen = 1;
        menuButtonController.index = 1;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator MenuQuit()
    {
        Application.Quit();
    }
}