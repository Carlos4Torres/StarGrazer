using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //For archiving purposes, this script must be accompanied by a box collider on CubeGrazer in order to work properly with interaction with enemy bullets.

    
    public int health;
    public AudioSource deathAudio;
    public RawImage healthUI;
    public Texture HP0, HP1, HP2, HP3, HP4, HP5;

    private void Start()
    {
        health = 5;
    }

    private void Update()
    {
        if (health == 5) { healthUI.texture = HP5; }
        if (health == 4) { healthUI.texture = HP4; }
        if (health == 3) { healthUI.texture = HP3; }
        if (health == 2) { healthUI.texture = HP2; }
        if (health == 1) { healthUI.texture = HP1; }
        if (health == 0) { healthUI.texture = HP0; }
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            deathAudio.Play();
            Destroy(gameObject);
        }
    }
}
