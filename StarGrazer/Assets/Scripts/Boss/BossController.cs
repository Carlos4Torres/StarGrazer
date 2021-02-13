using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossController : MonoBehaviour
{

    [Header("Movement Values")]
    public float MoveSpeed;
    private float moveDampen = 0;


    public enum shootingPattern
    {
        //Boss 1
        HUNGER_GAMES,
        HOTHEAD,
        WHAMMY_POP,
        THIS_IS_VIOLENCE_NOW,
        GUILLOTINE,
    }

    [Header("Shooting")]
    public float timeBetweenShots;
    public float initialShotDelay;
    public float shotSpeed = 25;
    public float health_per_phase;
    public float health;
    public float phases;
    public float dmgPerBullet; //Damage the boss takes per shot. Ideally would attach this value to the bullet instead of the boss
    public int boss; //which boss it is
    public int state; //0 is idle, 1, is move, 2 is combat. There was no reason for this to be an enum

    [Header("Components")]
    public Transform shotSpawn;
    public shootingPattern pattern;
    public GameObject bc;
    public GameObject[] BulletTypes;
    public Sprite[] BulletSprites;
    public Color[] colors;
    public CinemachineDollyCart mainDollyScipt; //Using this just to match speed
    private CinemachineDollyCart localDollyScript;

    //timer variable and the timer number it resets to when completed
    public int timer;
    public int timertop;
    public int timer2;
    public int timertop2;

    //attack specific variables
    public GameObject bomb;
    public bool burst;
    public float rise = -20;
    public int BladePos;

    public int secondstowait;
    //-----------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        localDollyScript = GetComponent<CinemachineDollyCart>();
        state = 0;
        localDollyScript.m_Speed = 0;
    }

    void Update()
    {
        //if the boss's health is 0, but they have more than 1 phase left, transitions to the next phase
        if ((health <= 0) && (phases > 0)) { phases--; health = health_per_phase; }

        //if the boss's health is 0 and they have no more phases left, kills the boss.
        if ((health <= 0) && (phases == 0)) { Destroy(this.gameObject); }


        //Moves the boss object into the scene, Once it gets there, it transitions to its combat state I think
        if (state == 1) {
            localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, 5, moveDampen);
            moveDampen += 0.007f * Time.deltaTime;

            if ((localDollyScript.m_Position - mainDollyScipt.m_Position < 40) && (state != 2))
            { localDollyScript.m_Speed = mainDollyScipt.m_Speed; moveDampen = 0; state = 2; }
        }


        if (state == 2) { timer--; timer2--; if (timer <= 0) { Attack(); timer = timertop; } }


        //checks for which boss it is so I can just reuse this script for all of the bosses
        if (boss == 1)
        {
            //checks the phase of the boss/
            if (phases == 5) { pattern = shootingPattern.HUNGER_GAMES; timertop = 20; }
            if (phases == 4) { pattern = shootingPattern.HOTHEAD; timertop = 20; timertop2 = 120; }
            if (phases == 3) { pattern = shootingPattern.WHAMMY_POP; timertop = 20; timertop2 = 180; rise = -20; }
            if (phases == 2) { pattern = shootingPattern.THIS_IS_VIOLENCE_NOW; timertop = 240; }
            if (phases == 1) { pattern = shootingPattern.GUILLOTINE; timertop = 40; timertop2 = 300; }
        }
        else if( boss == 2)
        {
            //just copy pasted this part in bc it froze without it 
            if (phases == 5) { pattern = shootingPattern.HUNGER_GAMES; timertop = 20; }
            if (phases == 4) { pattern = shootingPattern.HOTHEAD; timertop = 20; timertop2 = 120; }
            if (phases == 3) { pattern = shootingPattern.WHAMMY_POP; timertop = 20; timertop2 = 180; rise = -20; }
            if (phases == 2) { pattern = shootingPattern.THIS_IS_VIOLENCE_NOW; timertop = 240; }
            if (phases == 1) { pattern = shootingPattern.GUILLOTINE; timertop = 40; timertop2 = 300; }
        }
        else if (boss == 3)
        {

            //Change the position on the "Gameplay Plane" object script to 2800 for easy testing the boss section only

            //just copy pasted this part in bc it froze without it 
            if (phases == 5) { pattern = shootingPattern.HUNGER_GAMES; timertop = 20; }
            if (phases == 4) { pattern = shootingPattern.HOTHEAD; timertop = 20; timertop2 = 120; }
            if (phases == 3) { pattern = shootingPattern.WHAMMY_POP; timertop = 20; timertop2 = 180; rise = -20; }
            if (phases == 2) { pattern = shootingPattern.THIS_IS_VIOLENCE_NOW; timertop = 240; }
            if (phases == 1) { pattern = shootingPattern.GUILLOTINE; timertop = 40; timertop2 = 300; }


            //Used to stop the boss/player once they reach the end of the boss tunnel. Might also be useful to use this code in level 1 to prevent the teleport issue. 
            //Once you put in whatever code you want to use to signifiy the phase two/retreating camera section, I can come back in and make the player & boss start going backwards.
            if (localDollyScript.m_Position > localDollyScript.m_Path.PathLength - 150)
           {
               localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, 0, moveDampen);
               mainDollyScipt.m_Speed = Mathf.Lerp(mainDollyScipt.m_Speed, 0, moveDampen);
    
               moveDampen += 0.007f * Time.deltaTime;
           }
        }


    }

    //--------------------------------------------------------------------------------------------------------------------

    //move in function
    public void Entry() { state = 1; localDollyScript.m_Speed = MoveSpeed; }


    //detects when the player gets to the boss area in order to activate it
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == 0)
            {
                if(boss != 2)
                {
                    if (localDollyScript.m_Position - mainDollyScipt.m_Position > 40)
                        MoveSpeed += ((localDollyScript.m_Position - mainDollyScipt.m_Position) * -.75f);      
                }

                var collider = this.GetComponent<BoxCollider>();
                collider.enabled = false;

                Entry();
            }
        }

        //when bullets hit the boss, they are destroyed
        if (other.CompareTag("Bullet"))
        {
            if (state == 2)
            {
                health -= dmgPerBullet;
                Destroy(other.gameObject);
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------


    public void Attack()
    {
        if (pattern == shootingPattern.HUNGER_GAMES) {
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 0, Random.Range(-1f, -5f));
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 2.0f, 0, 0, Random.Range(-1f, -5f));
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 0, Random.Range(-1f, -5f));
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 2.0f, 0, 0, Random.Range(-1f, -5f));
        }


        if (pattern == shootingPattern.HOTHEAD) {

            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 1.0f, 0, 0, 0);
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 1.0f, 0, 0, 0);
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 1.0f, 0, 0, 0);
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 1.0f, 0, 0, 0);
            if (timer2 <= 0) { bc.GetComponent<BulletController>().ShootExtra(BulletTypes[1], shotSpawn, 0, Random.Range(-10f, 10f), 40, 0, 90, 0, BulletSprites[4], colors[2], 2.0f, 0, 0, -30); timer2 = timertop2; }


        }


        if (pattern == shootingPattern.WHAMMY_POP)
        {
            if (timer2 <= 0) { GameObject thisone = Instantiate(bomb, shotSpawn.position, Quaternion.Euler(0, 0, 0)); thisone.GetComponent<EnemyShootFollow>().mode = 1; timer2 = timertop2; }
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[3], 2.0f, 0, 3, -2f);
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[3], 2.0f, 0, -3, -2f);

        }

        if (pattern == shootingPattern.THIS_IS_VIOLENCE_NOW)
        {
            StartCoroutine(TIVN());
        }


        if (pattern == shootingPattern.GUILLOTINE)
        {
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -5.5f, 0, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 0, -2f);
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 5.5f, 0, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 0, -2f);

            if (timer2 <= 0)
            {
                BladePos = Random.Range(0, 3);
                if (BladePos == 0) bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 11, 0, 0, 0, 0, BulletSprites[8], colors[2], 1.5f, 0, 0, 20); secondstowait = 3; StartCoroutine(G());
                if (BladePos == 1) { bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[2], 1.5f, 0, 0, 20); secondstowait = 3; StartCoroutine(G()); }
                if (BladePos == 2) { bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -11, 0, 0, 0, 0, BulletSprites[8], colors[2], 1.5f, 0, 0, 20); secondstowait = 3; StartCoroutine(G()); }
                timer2 = timertop2;
            }
        }
    }
        


    IEnumerator G()
    {
        yield return new WaitForSeconds(secondstowait);
        if (BladePos == 0) { bc.GetComponent<BulletController>().ShootExtra(BulletTypes[3], shotSpawn, 0, 11, 0, 0, -90, 0, BulletSprites[5], colors[2], 1.5f, 0, 0, -40); }
        if (BladePos == 1) { bc.GetComponent<BulletController>().ShootExtra(BulletTypes[3], shotSpawn, 0, 0, 0, 0, -90, 0, BulletSprites[5], colors[2], 1.5f, 0, 0, -40); }
        if (BladePos == 2) { bc.GetComponent<BulletController>().ShootExtra(BulletTypes[3], shotSpawn, 0, -11, 0, 0, -90, 0, BulletSprites[5], colors[2], 1.5f, 0, 0, -40); }
    }



    IEnumerator TIVN()
    {
        for (int i = 0;i<50;i++)
        {
            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[2], shotSpawn, 0, rise, Random.Range(-5f, 0f), 0, 0, 0, BulletSprites[0], colors[0], Random.Range(0.5f,3.0f), 0, 0, 0);
            rise += 1.25f;
        }
        yield return new WaitForSeconds(1);
        burst = true;
        yield return new WaitForSeconds(1);
        burst = false; rise = -20;
        yield return new WaitForSeconds(3);
    }
}
