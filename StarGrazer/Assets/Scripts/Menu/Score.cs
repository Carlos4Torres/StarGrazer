using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int score;
    public GameObject[] Enemy;
    [SerializeField] GameObject empty;
    bool done = false;
    private int highScr;

    public Text scoreText;
    public Text highScore;

    void Start()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Enemy Model");
        PlayerPrefs.SetInt("score", 0);
        highScr = PlayerPrefs.GetInt("highscore");
        highScore.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    //This method uses an array populated with the enemies in each level to run a for loop that constantly checks every frame for a destroyed enemy in the array and adds points to the score variable accordingly
    void Update()
    {
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
        for (int i = 0; i < Enemy.Length; i++)
        {
            if (Enemy[i] == null)
            {
                Enemy[i] = empty;
            }
            if (Enemy[i] != Enemy[i] && Enemy[i] != empty)
            {
                Enemy[i] = empty;
            }

            if (Enemy[i] != empty)
            {
                if (Enemy[i].GetComponent<MeshRenderer>().enabled == false)
                {

                    PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);
                    Enemy[i] = empty;
                }
            }


        }

        if (PlayerPrefs.GetInt("score") >= highScr)
        {
            PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
            highScore.text = PlayerPrefs.GetInt("highscore").ToString();
        }

    }
}