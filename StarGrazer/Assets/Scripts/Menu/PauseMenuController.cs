﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static int pauseindex;
    public Canvas PauseMenu;
    public bool isPaused;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex = 1;
    [SerializeField] int minIndex = 4;

    private void Start()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
            //if(isPaused)
            //{
                //ResumeGame();
                //pauseindex = 1;
            //}
            //else
            //{
                //PauseGame();
                //pauseindex = 1;
            //}
        //}
        if (PauseMenu.GetComponent<Canvas>().enabled == true)
        {
            //if (Input.GetKeyDown(KeyCode.S))
            //{
                //if (pauseindex < maxIndex)
                //{
                    //pauseindex++;
                //}
                //else
                //{
                    //pauseindex = minIndex;
                //}
                //if (pauseindex > maxIndex || pauseindex < minIndex)
                //{
                    //pauseindex = minIndex;
                //}
            //}
            //else if (Input.GetKeyDown(KeyCode.W))
            //{
                //if (pauseindex > minIndex)
                //{
                    //pauseindex--;
                //}
                //else
                //{
                    //pauseindex = maxIndex;
                //}
                //if (pauseindex < minIndex || pauseindex > maxIndex)
                //{
                        //pauseindex = minIndex;
                //}
            //}

            if (Input.GetKeyDown(KeyCode.Space) && pauseindex == 1)
            {
                ResumeGame();
            }
            if (Input.GetKeyDown(KeyCode.Space) && pauseindex == 2)
            {
                RestartGame();
            }
            if (Input.GetKeyDown(KeyCode.Space) && pauseindex == 3)
            {
                MenuMainMenu();
            }
            if (Input.GetKeyDown(KeyCode.Space) && pauseindex == 4)
            {
                QuitGame();
            }
        }
    }

    public void PauseGame()
    {
        PauseMenu.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void MenuMainMenu()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}