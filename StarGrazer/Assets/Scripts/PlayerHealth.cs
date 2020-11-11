using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //For archiving purposes, this script must be accompanied by a box collider on CubeGrazer in order to work properly with interaction with enemy bullets.

    public int health;
    public AudioSource deathAudio;

    public void Demage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            deathAudio.Play();
            Destroy(gameObject);
        }
    }
}
