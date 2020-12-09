using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //For archiving purposes, this script must be accompanied by a box collider on CubeGrazer in order to work properly with interaction with enemy bullets.

    
    public int health = 5;
    public AudioSource deathAudio;
    public RawImage healthUI;
    public Texture[] healthImages;
    //Cleaned this up to be an array, only updates when damaged instead of updating every frame


    public void Start()
    {
        health = 5;
        healthUI.texture = healthImages[health];
    }

    public void Damage()
    {
        health--;

        healthUI.texture = healthImages[health];

        if (health <= 0)
        {
            deathAudio.Play();
            Destroy(gameObject);
        }
    }

   
}
