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
            maxIndex = 4;
        }
        if (menuScreen == 2)
        {
            minIndex = 5;
            maxIndex = 8;
        }
        if (menuScreen == 3)
        {
            minIndex = 9;
            maxIndex = 9;
        }
        if (menuScreen == 4)
        {
            minIndex = 10;
            maxIndex = 10;
        }
    }
}
