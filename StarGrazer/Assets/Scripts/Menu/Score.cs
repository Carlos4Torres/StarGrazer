using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] GameObject empty;
    GameObject[] Enemy;

    bool done = false;

    public Text scoreText;

    void Start()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Enemy Model");
    }


    void Update()
    {
        scoreText.text = score.ToString();

        //This method uses an array populated with the enemies in each level to run a for loop that constantly checks every frame for a destroyed enemy in the array and adds points to the score variable accordingly
        for(int i=0; i<Enemy.Length; i++)
        {
            if (Enemy[i] != empty)
            {
                if (Enemy[i].GetComponent<MeshRenderer>().enabled == false)
                {

                    score = score + 10;
                    Enemy[i] = empty;   
                }
            }
          
        }

        if (!GameObject.FindGameObjectWithTag("Boss") && done == false)
        {
            score = score + 100;
            done = true;
        }

    }
}
