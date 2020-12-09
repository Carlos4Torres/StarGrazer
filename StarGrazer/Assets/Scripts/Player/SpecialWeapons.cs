using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWeapons : MonoBehaviour
{
    [Range(0, 10)]
    public static int specialWeaponUnits;
    public bool grazed;

    private int grazes;

    PlayerControls controls;


    //gamePlayPause DataField use for Slow Affect
    public float gamePlayPause = 0f;
    //pauseTime field for how long Slow Affect will
    public float pauseTime = 2f;


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
        specialWeaponUnits = 3;
        grazed = false;
        grazes = 0;
    }

    void FixedUpdate()
    {
        if (grazed && grazes < 99)         //if graze and grazes < 99, add 1 to grazes.
        {
            grazes++;//=10;
            print("Grazes: " + grazes);
            grazed = false;
        }
        else if (grazed && grazes >= 99)   //if graze and grazes >= 99, reset to 0 add 1 graze and add 1 specialweaponunit.
        {
            grazes = 0;
            print("Grazes Reset: " + grazes);
            grazed = false;
            if (specialWeaponUnits < 10)
            {
                specialWeaponUnits++;
                print("Unit added.\nUnits Left: " + specialWeaponUnits);
            }
            else if (specialWeaponUnits >= 10)
            {
                print("reached maximum units");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Start GamePlaySlow Coroutine
          
          StartCoroutine(GamePlaySlow());
        }

  
    }

    void FireTimeStopper() //If more than 0 special weapon units, fire Time Stopper.
    {
        if (specialWeaponUnits > 0)
        {
            specialWeaponUnits--;
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
            specialWeaponUnits--;
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
