using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialWeapons : MonoBehaviour
{
    private int specialWeaponUnits;
    private const int MAXSPECIAL = 3;
    public Animator animator;

    [Header("Images")]
    public RawImage specialUI;
    public Texture[] specialImages;

    private bool grazed;
    private int grazes;

    PlayerControls controls;

    //Anti-Matter Bomb
    private GameObject[] enemies;
    private GameObject[] enemyBullets;


    //gamePlayPause DataField use for Slow Affect
    public float gamePlayPause = 0.005f;
    //pauseTime field for how long Slow Affect will
    public float pauseTime = .01f;


    //Necessary for Input System
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    //Necessary for Input System
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    //Checks if special weapon buttons have been pressed
    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.SpecialWeapon_TimeStopper.started += ctx => FireTimeStopper();
        controls.Gameplay.SpecialWeapon_AntiMatterBomb.started += ctx => FireAntiMatterBomb();
    }

    void Start()
    {
        specialWeaponUnits = MAXSPECIAL;
        specialUI.texture = specialImages[specialWeaponUnits];
        grazed = false;
        grazes = 0;
    }

    void FixedUpdate()
    {
        if (grazed && grazes < 99)         //if graze and grazes < 99, add 1 to grazes.
        {
            grazes += 10;
            print("Grazes: " + grazes);
            grazed = false;
        }
        else if (grazed && grazes >= 99)   //if graze and grazes >= 99, reset to 0 add 1 graze and add 1 specialweaponunit.
        {
            grazes = 0;
            print("Grazes Reset: " + grazes);
            grazed = false;
            if (specialWeaponUnits < MAXSPECIAL)
            {
                specialWeaponUnits++;
                specialUI.texture = specialImages[specialWeaponUnits];
                print("Unit added.\nUnits Left: " + specialWeaponUnits);
            }
            else if (specialWeaponUnits >= 10)
            {
                print("reached maximum units");
            }
        }

    }


    void FireTimeStopper() //If more than 0 special weapon units, fire Time Stopper.
    {
        if (specialWeaponUnits > 0)
        {
            specialWeaponUnits--;
            specialUI.texture = specialImages[specialWeaponUnits];
            StartCoroutine(GamePlaySlow());
            print("Time Stopper. Units Left: " + specialWeaponUnits);
        }
        else
        {
            print("Not enough special weapon units.");
        };
    }

    void FireAntiMatterBomb() //If more than 0 special weapon units, fire Anti-Matter Bomb.
    {
        if (specialWeaponUnits > 0)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                print("bomb used");
                animator.SetTrigger("boom");
            }

            specialWeaponUnits--;
            specialUI.texture = specialImages[specialWeaponUnits];

            enemies = GameObject.FindGameObjectsWithTag("Enemy Model");
            foreach (GameObject enemy in enemies)
            {
                Renderer renderer = enemy.GetComponent<Renderer>();
                if(renderer.isVisible)
                {
                    Destroy(enemy.transform.parent.gameObject);
                }
            }

            enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            foreach (GameObject bullet in enemyBullets)
            {
                Destroy(bullet.gameObject);
            }

            print("Anti-Matter. Units Left: " + specialWeaponUnits);
        }
        else
        {
            print("Not enough special weapon units.");
        }
    }

    //Coroutine for Time Stop Affect
    IEnumerator GamePlaySlow()
    {
        Time.timeScale = gamePlayPause;
        yield return new WaitForSeconds(pauseTime);
        Time.timeScale = 1;
    }
}
