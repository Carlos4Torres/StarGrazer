using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    [SerializeField] int minIndex;
    [SerializeField] int menuScreen;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        menuScreen = MenuButton.menuScreen;
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                    }
                    else
                    {
                        index = minIndex;
                    }
                    if (index > maxIndex || index < minIndex)
                    {
                        index = minIndex;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > minIndex)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxIndex;
                    }
                    if (index < minIndex || index > maxIndex)
                    {
                        index = minIndex;
                    }
                }
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
        if (menuScreen == 1)
        {
            minIndex = 1;
            maxIndex = 5;
        }
        if (menuScreen == 2)
        {
            minIndex = 6;
            maxIndex = 9;
        }
        if (menuScreen == 3)
        {
            minIndex = 10;
            maxIndex = 10;
        }
        if (menuScreen == 4)
        {
            minIndex = 11;
            maxIndex = 11;
        }
        if (menuScreen == 5)
        {
            minIndex = 12;
            maxIndex = 18;
        }
        if (menuScreen == 6)
        {
            minIndex = 19;
            minIndex = 19;
        }
    }
}
