using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHighScore : MonoBehaviour
{
    public Text highScore;

    private void Update()
    {
        highScore.text = PlayerPrefs.GetInt("highscore").ToString();
    }
}
