using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] int thisIndex;
    [SerializeField] public static int menuScreen;
    [SerializeField] public static int levelSelect;
    [SerializeField] public static int clickControl;
    public GameObject levelText;
    public GameObject levelDescription;

    public void Start()
    {
        menuScreen = 1;
        clickControl = 0;
        levelSelect = 0;
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
                        StartCoroutine(MenuMainToGameMode());
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
                        StartCoroutine(MenuMainToCredits());
                    }  
                    if (menuButtonController.index == 5 && thisIndex == 5)
                    {
                        StartCoroutine(MenuQuit());
                    }
                }
                if (menuScreen == 2) //Game Mode Menu
                {
                    if (menuButtonController.index == 6 && thisIndex == 6)
                    {
                        StartCoroutine(MenuNewGame());
                    }
                    if (menuButtonController.index == 7 && thisIndex == 7)
                    {
                        StartCoroutine(MenuLevelSelect());
                    }
                    if (menuButtonController.index == 8 && thisIndex == 8)
                    {
                        StartCoroutine(MenuGameModeToMain());
                    }
                }
                if (menuScreen == 3) // Options Menu
                {
                    if (menuButtonController.index == 9 && thisIndex == 9)
                    {
                        StartCoroutine(MenuSound());
                    }
                    if (menuButtonController.index == 10 && thisIndex == 10)
                    {
                        StartCoroutine(MenuFullScreen());
                    }
                    if (menuButtonController.index == 11 && thisIndex == 11)
                    {
                        StartCoroutine(MenuOptionsToMain());
                    }
                }
                if (menuScreen == 4) // HighScores Menu
                {
                    if (menuButtonController.index == 12 && thisIndex == 12)
                    {
                        StartCoroutine(MenuHighScoresToMain());
                    }
                }
                if (menuScreen == 5) // Level Select Menu
                {
                    if (menuButtonController.index == 13 && thisIndex == 13)
                    {
                        StartCoroutine(MenuLevelSelectLevel1());
                        levelSelect = 1;
                    }
                    if (menuButtonController.index == 14 && thisIndex == 14)
                    {
                        StartCoroutine(MenuLevelSelectLevel2());
                        levelSelect = 2;
                    }
                    if (menuButtonController.index == 15 && thisIndex == 15)
                    {
                        StartCoroutine(MenuLevelSelectLevel3());
                        levelSelect = 3;
                    }
                    if (menuButtonController.index == 16 && thisIndex == 16)
                    {
                        StartCoroutine(MenuLevelSelectLevel4());
                        levelSelect = 4;
                    }
                    if (menuButtonController.index == 17 && thisIndex == 17)
                    {
                        StartCoroutine(MenuLevelSelectLevel5());
                        levelSelect = 5;
                    }
                    if (menuButtonController.index == 18 && thisIndex == 18)
                    {
                        StartCoroutine(MenuLevelSelectStart());
                    }
                    if (menuButtonController.index == 19 && thisIndex == 19)
                    {
                        StartCoroutine(MenuLevelSelectToGameMode());
                    }
                }
                if (menuScreen == 6) // Credits
                {
                    if (menuButtonController.index == 20 && thisIndex == 20)
                    {
                        StartCoroutine(MenuCreditsToMain());
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

    //Added in scene openers for each of the levels
    //Didn't rename the IENumerators to level specific ones in case we want to change them back to their intended purposes 

    public IEnumerator MenuMainToGameMode()
    {
        clickControl = 1;
        menuScreen = 2;
        menuButtonController.index = 6;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuNewGame()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level One (Updated Visuals)");
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuLevelSelect()
    {
        clickControl = 1;
        menuScreen = 5;
        menuButtonController.index = 13;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuGameModeToMain()
    {
        clickControl = 1;
        menuScreen = 1;
        menuButtonController.index = 1;
        yield return new WaitForSeconds(0.5f);
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
    
    public IEnumerator MenuSound()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("selected", false);
        clickControl = 0;
    }
    
    public IEnumerator MenuFullScreen()
    {
        clickControl = 1;
        menuScreen = 3;
        menuButtonController.index = 9;
        Screen.fullScreen = !Screen.fullScreen;
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
        menuButtonController.index = 12;
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

    public IEnumerator MenuMainToCredits()
    {
        clickControl = 1;
        menuScreen = 6;
        menuButtonController.index = 20;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuCreditsToMain()
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

    public IEnumerator MenuLevelSelectLevel1()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        levelText.GetComponent<Text>().text = "L E V E L   1: Warzone Metropolis";
        levelDescription.GetComponent<Text>().text = "The most populated city on MC-3654T, transformed into a warzone filled with combat robots and factories. There is noticeable propaganda littering the buildings, accenting the Riders’ pride.";
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuLevelSelectLevel2()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        levelText.GetComponent<Text>().text = "L E V E L   2: Steamy Grotto";
        levelDescription.GetComponent<Text>().text = "A set of damp caverns underneath the city that is constantly creating steam due to the heat of the magma ocean below. The caves are only accessible through the Warzone Metropolis’ hidden tunnels.";
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuLevelSelectLevel3()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        levelText.GetComponent<Text>().text = "LE V E L   3: Magma Ocean";
        levelDescription.GetComponent<Text>().text = "A vast, underground ocean of lava. The Riders’ main science facilities are attached to the roofing of these caves.";
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuLevelSelectLevel4()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        levelText.GetComponent<Text>().text = "LE V E L   4: Epitome Temple";
        levelDescription.GetComponent<Text>().text = "A dark, ghostly temple hidden in the dark pockets of the magma sea. This temple is where Epitomes were discovered and channeled from the elite Riders.";
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuLevelSelectLevel5()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        levelText.GetComponent<Text>().text = "L E V E L   5: Subterranean Valley";
        levelDescription.GetComponent<Text>().text = "Hidden beneath the Epitome Temple is a golden land filled with hills and fields, lit by an artificial sun. It is the last line of defense of the only entrance to the planet’s core.";
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuLevelSelectStart()
    {
        clickControl = 1;
        yield return new WaitForSeconds(0.5f);
        if (levelSelect == 1)
        {
            SceneManager.LoadScene("Level One (Updated Visuals)");
        }
        if (levelSelect == 2)
        {
            SceneManager.LoadScene("(WB) Level Two");
        }
        if (levelSelect == 3)
        {
            SceneManager.LoadScene("(WB) Level 3");
        }
        if (levelSelect == 4)
        {
            SceneManager.LoadScene("(WB) Level 4");
        }
        if (levelSelect == 5)
        {
            SceneManager.LoadScene("(WB) Level 5");
        }
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public IEnumerator MenuLevelSelectToGameMode()
    {
        clickControl = 1;
        menuScreen = 2;
        levelSelect = 0;
        menuButtonController.index = 6;
        yield return new WaitForSeconds(0.5f);
        levelText.GetComponent<Text>().text = "S E L E C T   A   L E V E L";
        levelDescription.GetComponent<Text>().text = "";
        animator.SetBool("selected", false);
        clickControl = 0;
    }

    public void button_click()
    {
        if (menuScreen == 1)
        {
            if (thisIndex == 1)
            {
                StartCoroutine(MenuMainToGameMode());
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
                StartCoroutine(MenuMainToCredits());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 5)
            {
                StartCoroutine(MenuQuit());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
        }
        if (menuScreen == 2)
        {
            if (thisIndex == 6)
            {
                StartCoroutine(MenuNewGame());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 7)
            {
                StartCoroutine(MenuLevelSelect());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 8)
            {
                StartCoroutine(MenuGameModeToMain());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
        }
        if (menuScreen == 3)
        {
            if (thisIndex == 9)
            {
                StartCoroutine(MenuSound());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 10)
            {
                StartCoroutine(MenuFullScreen());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 11)
            {
                StartCoroutine(MenuOptionsToMain());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
        }
        if (menuScreen == 4)
        {
            if (thisIndex == 12)
            {
                StartCoroutine(MenuHighScoresToMain());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
        }
        if (menuScreen == 5)
        {
            if (thisIndex == 13)
            {
                StartCoroutine(MenuLevelSelectLevel1());
                levelSelect = 1;
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 14)
            {
                StartCoroutine(MenuLevelSelectLevel2());
                levelSelect = 2;
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 15)
            {
                StartCoroutine(MenuLevelSelectLevel3());
                levelSelect = 3;
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 16)
            {
                StartCoroutine(MenuLevelSelectLevel4());
                levelSelect = 4;
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 17)
            {
                StartCoroutine(MenuLevelSelectLevel5());
                levelSelect = 5;
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 18)
            {
                StartCoroutine(MenuLevelSelectStart());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
            if (thisIndex == 19)
            {
                StartCoroutine(MenuLevelSelectToGameMode());
                animator.SetBool("selected", true);
                animator.SetBool("pressed", true);
            }
        }
        if (menuScreen == 6)
        {
            if (thisIndex == 20)
            {
                StartCoroutine(MenuCreditsToMain());
                animator.SetBool("selected", true);
                animator.SetBool("selected", true);
            }
        }
    }
}