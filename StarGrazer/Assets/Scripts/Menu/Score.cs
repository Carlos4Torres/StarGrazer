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

    public Text scoreText;


    void Start()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Enemy Model");
    }

    //This method uses an array populated with the enemies in each level to run a for loop that constantly checks every frame for a destroyed enemy in the array and adds points to the score variable accordingly
    void Update()
    {
        scoreText.text = score.ToString();
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

                    score = score + 10;
                    Enemy[i] = empty;
                }
            }


        }

    }
}