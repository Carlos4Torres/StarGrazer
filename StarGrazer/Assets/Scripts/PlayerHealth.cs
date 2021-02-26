using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //For archiving purposes, this script must be accompanied by a box collider on CubeGrazer in order to work properly with interaction with enemy bullets.

    public AudioSource deathAudio;
    public GameObject model;

    [Header("Health")]
    public int health = 5;
    public RawImage healthUI;
    public Texture[] healthImages;

    

    [Header("Lives")]
    public int lives = 3;
    public RawImage livesUI;
    public Texture[] livesImages;
    public GameObject crosshair;

    private Collider healthCollider;


    public bool respawning;
    private readonly int respawnCycles = 10;
    
    public bool flickering;
    private readonly int flickerCycles = 3;

    public void Start()
    {
        Weapon.alive = true;
        respawning = false;
        health = 5;
        healthUI.texture = healthImages[health];

        lives = 3;
        livesUI.texture = livesImages[lives];

        healthCollider = this.GetComponent<Collider>();
    }

    public void Damage(int amount)
    {
        health -= amount;

        healthUI.texture = healthImages[health];

        if (health <= 0)
        {
            lives--;
            livesUI.texture = livesImages[lives];

            if(lives != 0)
            {
                StartCoroutine(Respawn());
            }
            else if(lives == 0)
            {
                StartCoroutine(Restart());
            }
        }
        // else StartCoroutine(Flicker());
        // We can put this here to give the player a bit of time to sort themselves between getting it, so they don't just lose all their health at once.
        // THe game is pretty dang hard right now, so I think adding this snippet it would be a lot of help, but need to talk to other before fully implementing.  
    }

    public IEnumerator Respawn()
    {
        respawning = true;

        healthCollider.enabled = false;

        for (int i = 0; i < respawnCycles; i++)
        {
            model.SetActive(false);
            yield return new WaitForSeconds(.2f);
            model.SetActive(true);
            yield return new WaitForSeconds(.2f);
        }

        health = 5;
        healthUI.texture = healthImages[health];

        healthCollider.enabled = true;
        respawning = false;
    }

    //Needed for when player hits an enemy. 
    public IEnumerator Flicker()
    {
        flickering = true;
        healthCollider.enabled = false;

        for (int i = 0; i < flickerCycles; i++)
        {
            model.SetActive(false);
            yield return new WaitForSeconds(.2f);
            model.SetActive(true);
            yield return new WaitForSeconds(.2f);
        }

        healthCollider.enabled = true;
        flickering = false;
    }

    public IEnumerator Restart()
    {
        Weapon.alive = false;
        healthCollider.enabled = false;
        deathAudio.Play();
        model.SetActive(false);
        crosshair.SetActive(false);
        //Destroy(gameObject);

        yield return new WaitForSeconds(3f);
        Weapon.alive = true;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
   
}
