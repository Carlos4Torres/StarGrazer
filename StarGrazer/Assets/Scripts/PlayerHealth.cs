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

    private Collider healthCollider;
    private int respawnCycles = 10;

    //Cleaned this up to be an array, only updates when damaged instead of updating every frame

    public void Start()
    {
        health = 5;
        healthUI.texture = healthImages[health];

        lives = 3;
        livesUI.texture = livesImages[lives];

        healthCollider = this.GetComponent<Collider>();
    }

    public void Damage()
    {
        health--;

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
    }

    public IEnumerator Respawn()
    {

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
    }

    private IEnumerator Restart()
    {

        deathAudio.Play();
        Destroy(gameObject);

        yield return new WaitForSeconds(3f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
   
}
