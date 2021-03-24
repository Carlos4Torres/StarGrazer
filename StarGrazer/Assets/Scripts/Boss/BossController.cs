using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        //Boss 2
        THE_FEVER,
        STREAKY,
        FULL_MOON,
        CUT_THROAT,
        THE_FEAR,
        //Boss 3
        LIL_BOY,
        DEATH_HEATED,
        SYSTEM_BLOWER,
        HACKER,
        EIGHT_OH_EIGHT,
        ARTIFICIAL_DEATH,
        //Boss 4
        INANIMATE_SENSATION,
        HAHAHA,
        THRU_THE_WALLS,
        LOST_BOYS,
        BIRDS,
        HUSTLE_BONES,
        //Boss 5
        ALL_MY_LIFE,
        MORE_THAN_THE_FAIRY,
        CENTURIES_OF_DAMN,
        GIVING_BAD_PEOPLE_GOOD_IDEAS,
        TWO_HEAVENS,
        BEYOND_ALIVE,
        ON_GP
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
    public int phasetimer;

    //attack specific variables
    public GameObject bomb;
    public bool burst;
    public float rise = -20;
    public int BladePos;

    public float randval;

    public int secondstowait;

    public RawImage m_RawImage;
    public float alpha;
    public Color tempcolor;

    private bool deathActivated;
    public GameObject modelObj;

    //-----------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        alpha = 0f;
        localDollyScript = GetComponent<CinemachineDollyCart>();
        state = 0;
        localDollyScript.m_Speed = 0;
    }

    void Update()
    {
        //Cut Ins
        //run this every frame to check for whether its active or not
        //create a temporary color variable
        tempcolor = m_RawImage.color;
        //set the alpha of that temporary color variable based on the CIactive variable
        tempcolor.a = alpha;
        //apply the temporary color to the real cut in
        m_RawImage.color = tempcolor;

        //if CIactive is on, add to the alpha. If it's off, subtract.
        if ((phasetimer > 100) && (alpha < 255f)){alpha += 1f;}
        if ((phasetimer <= 100) && (alpha > 0f)) {alpha -= 1f;}




        //constantly decreases timer that controls the pause between phases
        if (phasetimer > 0) { phasetimer--; }

        //if the boss's health is 0, but they have more than 1 phase left, transitions to the next phase
        if ((health <= 0) && (phases > 0)) 
        {   phasetimer = 500;  
            phases--; 
            if(phases!= 0 ) health = health_per_phase; 
        }

        //if the boss's health is 0 and they have no more phases left, kills the boss.
        if ((health <= 0) && (phases == 0) && !deathActivated)
        {
            deathActivated = true;
            StartCoroutine(Death());
        }


        //Moves the boss object into the scene, Once it gets there, it transitions to its combat state I think
        switch (state)
        {
            case 1:
                localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, 5, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;

                if ((localDollyScript.m_Position - mainDollyScipt.m_Position < 40) && (state != 2))
                { localDollyScript.m_Speed = mainDollyScipt.m_Speed; moveDampen = 0; state = 2; }
                break;

            case 2:
                { timer--; timer2--; if ((timer <= 0) && (phasetimer <= 0)) { Attack(); timer = timertop; } }
                break;
        }


        //checks for which boss it is so I can just reuse this script for all of the bosses
        switch (boss)
        {
            case 1:
                switch (phases)
                {
                    case 5:
                        { pattern = shootingPattern.HUNGER_GAMES; timertop = 20; }
                        break;
                    case 4:
                        { pattern = shootingPattern.HOTHEAD; timertop = 20; timertop2 = 120; }
                        break;
                    case 3:
                        { pattern = shootingPattern.WHAMMY_POP; timertop = 20; timertop2 = 180; rise = -20; }
                        break;
                    case 2:
                        { pattern = shootingPattern.THIS_IS_VIOLENCE_NOW; timertop = 240; }
                        break;
                    case 1:
                        { pattern = shootingPattern.GUILLOTINE; timertop = 40; timertop2 = 300; }
                        break;
                }
                break;

            case 2:
                switch (phases)
                {
                    case 5:
                        { pattern = shootingPattern.THE_FEVER; timertop = 100; }
                        break;
                    case 4:
                        { pattern = shootingPattern.STREAKY; timertop = 70; }
                        break;
                    case 3:
                        { pattern = shootingPattern.FULL_MOON; timertop = 160; }
                        break;
                    case 2:
                        { pattern = shootingPattern.CUT_THROAT; timertop = 100; }
                        break;
                    case 1:
                        { pattern = shootingPattern.THE_FEAR; timertop = 100; }
                        break;
                }
                break;

            case 3:
                switch (phases)
                {
                    case 5:
                        { pattern = shootingPattern.LIL_BOY; timertop = 300; }
                        break;
                    case 4:
                        { pattern = shootingPattern.DEATH_HEATED; timertop = 150; }
                        break;
                    case 3:
                        { pattern = shootingPattern.SYSTEM_BLOWER; timertop = 20; }
                        break;
                    case 2:
                        { pattern = shootingPattern.HACKER; timertop = 20; }
                        break;
                    case 1:
                        { pattern = shootingPattern.ARTIFICIAL_DEATH; timertop = 60; }
                        break;
                }

                //Used to stop the boss/player once they reach the end of the boss tunnel. Might also be useful to use this code in level 1 to prevent the teleport issue. 
                //Once you put in whatever code you want to use to signifiy the phase two/retreating camera section, I can come back in and make the player & boss start going backwards.
                if (localDollyScript.m_Position > localDollyScript.m_Path.PathLength - 150)
                {
                    localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, 0, moveDampen);
                    mainDollyScipt.m_Speed = Mathf.Lerp(mainDollyScipt.m_Speed, 0, moveDampen);

                    moveDampen += 0.007f * Time.deltaTime;
                }
                break;

            case 4:
                switch (phases)
                {
                    case 5:
                        { pattern = shootingPattern.INANIMATE_SENSATION; timertop = 100; }
                        break;
                    case 4:
                        { pattern = shootingPattern.HAHAHA; timertop = 150; }
                        break;
                    case 3:
                        { pattern = shootingPattern.BIRDS; timertop = 100; }
                        break;
                    case 2:
                        { pattern = shootingPattern.THRU_THE_WALLS; timertop = 100; }
                        break;
                    case 1:
                        { pattern = shootingPattern.HUSTLE_BONES; timertop = 50; }
                        break;
                }

                break;

            case 5:
                switch (phases)
                {
                    case 6:
                        { pattern = shootingPattern.ALL_MY_LIFE; timertop = 6; }
                        break;

                    case 5:
                        { pattern = shootingPattern.CENTURIES_OF_DAMN; timertop = 100; }
                        break;
                    case 4:
                        { pattern = shootingPattern.GIVING_BAD_PEOPLE_GOOD_IDEAS; timertop = 5; }
                        break;
                    case 3:
                        { pattern = shootingPattern.TWO_HEAVENS; timertop = 9; }
                        break;
                    case 2:
                        { pattern = shootingPattern.BEYOND_ALIVE; timertop = 18; }
                        break;
                    case 1:
                        { pattern = shootingPattern.ON_GP; timertop = 40; }
                        break;
                }
                break;
        }

    }

    //--------------------------------------------------------------------------------------------------------------------

    //move in function
    public void Entry() { state = 1; localDollyScript.m_Speed = MoveSpeed; }

    public IEnumerator Death()
    {
        Destroy(modelObj);
        yield return new WaitForSeconds(3);
        switch(boss)
        {
            case 1:
                SceneManager.LoadScene("(WB) Level Two");
                break;
            case 2:
                SceneManager.LoadScene("(WB) Level 3");
                break;
            case 3:
                SceneManager.LoadScene("(WB) Level 4");
                break;
            case 4:
                SceneManager.LoadScene("(WB) Level 5");
                break;
            case 5:
                SceneManager.LoadScene("Menu");
                break;

        }
    }

    //detects when the player gets to the boss area in order to activate it
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == 0)
            {
                if(boss != 2 && boss != 5)
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
        switch (pattern)
        {
            case shootingPattern.HUNGER_GAMES:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 0, Random.Range(-1f, -5f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 2.0f, 0, 0, Random.Range(-1f, -5f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 0, Random.Range(-1f, -5f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 2.0f, 0, 0, Random.Range(-1f, -5f));
                break;

            case shootingPattern.HOTHEAD:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 1.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 1.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 1.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[1], 1.0f, 0, 0, 0);
                if (timer2 <= 0) { bc.GetComponent<BulletController>().ShootExtra(BulletTypes[1], shotSpawn, 0, Random.Range(-10f, 10f), 40, 0, 90, 0, BulletSprites[4], colors[2], 2.0f, 0, 0, -30); timer2 = timertop2; }
                break;

            case shootingPattern.WHAMMY_POP:
                if (timer2 <= 0) { GameObject thisone = Instantiate(bomb, shotSpawn.position, Quaternion.Euler(0, 0, 0)); thisone.GetComponent<EnemyShootFollow>().mode = 1; timer2 = timertop2; }
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[3], 2.0f, 0, 3, -2f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[3], 2.0f, 0, -3, -2f);
                break;

            case shootingPattern.THIS_IS_VIOLENCE_NOW:
                StartCoroutine(TIVN());
                break;

            case shootingPattern.GUILLOTINE:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -5.5f, 0, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 0, -2f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 5.5f, 0, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 0, -2f);

                if (timer2 <= 0)
                {
                    BladePos = Random.Range(0, 3);
                    switch (BladePos)
                    {
                        case 0:
                            bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 11, 0, 0, 0, 0, BulletSprites[8], colors[2], 1.5f, 0, 0, 20); secondstowait = 3; StartCoroutine(G());
                            break;
                        case 1:
                            { bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[2], 1.5f, 0, 0, 20); secondstowait = 3; StartCoroutine(G()); }
                            break;
                        case 2:
                            { bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -11, 0, 0, 0, 0, BulletSprites[8], colors[2], 1.5f, 0, 0, 20); secondstowait = 3; StartCoroutine(G()); }
                            break;
                    }
                    timer2 = timertop2;
                }
                break;

            case shootingPattern.THE_FEVER:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -20, 17, 0, 0, 0, BulletSprites[7], colors[0], 0.8f, 0, 0, -20);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -15, 17, 0, 0, 0, BulletSprites[7], colors[0], 0.8f, 0, 0, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 17, 0, 0, 0, BulletSprites[7], colors[0], 0.8f, 0, 0, -20);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -5, 17, 0, 0, 0, BulletSprites[7], colors[0], 0.8f, 0, 0, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 17, 0, 0, 0, BulletSprites[7], colors[0], 0.8f, 0, 0, -20);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 5, 17, 0, 0, 0, BulletSprites[7], colors[0], 0.8f, 0, 0, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 17, 0, 0, 0, BulletSprites[7], colors[0], 0.8f, 0, 0, -20);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 15, 17, 0, 0, 0, BulletSprites[7], colors[0], 0.8f, 0, 0, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -20, 20, 0, 0, 0, BulletSprites[7], colors[0], 0.8f, 0, 0, -20);
                break;

            case shootingPattern.STREAKY:
                randval = Random.Range(-20f, 20f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[1], 0.8f, 0, 0, -30);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[1], 0.8f, 0, 0, -25);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[1], 0.8f, 0, 0, -20);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[1], 0.8f, 0, 0, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[1], 0.8f, 0, 0, -10);
                break;

            case shootingPattern.FULL_MOON:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[0], colors[3], 13.0f, 0, 0, -20);
                break;

            case shootingPattern.CUT_THROAT:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -20, 17, 0, 0, 0, BulletSprites[7], colors[2], 0.8f, 0, 0, Random.Range(-17f, -25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -15, 17, 0, 0, 0, BulletSprites[7], colors[2], 0.8f, 0, 0, Random.Range(-17f, -25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 17, 0, 0, 0, BulletSprites[7], colors[2], 0.8f, 0, 0, Random.Range(-17f, -25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -5, 17, 0, 0, 0, BulletSprites[7], colors[2], 0.8f, 0, 0, Random.Range(-17f, -25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 17, 0, 0, 0, BulletSprites[7], colors[2], 0.8f, 0, 0, Random.Range(-17f, -25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 5, 17, 0, 0, 0, BulletSprites[7], colors[2], 0.8f, 0, 0, Random.Range(-17f, -25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 17, 0, 0, 0, BulletSprites[7], colors[2], 0.8f, 0, 0, Random.Range(-17f, -25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 15, 17, 0, 0, 0, BulletSprites[7], colors[2], 0.8f, 0, 0, Random.Range(-17f, -25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -20, 20, 0, 0, 0, BulletSprites[7], colors[2], 0.8f, 0, 0, Random.Range(-17f, -25f));
                break;

            case shootingPattern.THE_FEAR:
                randval = Random.Range(-20f, 20f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[4], 0.8f, 0, 0, -30);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[4], 0.8f, 0, 0, -25);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[4], 0.8f, 0, 0, -20);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[4], 0.8f, 0, 0, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[4], 0.8f, 0, 0, -10);

                randval = Random.Range(-20f, 20f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[4], 0.8f, 0, 0, -30);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[4], 0.8f, 0, 0, -25);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[4], 0.8f, 0, 0, -20);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[4], 0.8f, 0, 0, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[4], 0.8f, 0, 0, -10);
                break;

            case shootingPattern.INANIMATE_SENSATION:
                randval = Random.Range(-20f, 20f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[2], 0.8f, 0, 5, -30);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[2], 0.8f, 0, 5, -25);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[2], 0.8f, 0, 5, -20);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[2], 0.8f, 0, 5, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[2], 0.8f, 0, 5, -10);

                randval = Random.Range(-20f, 20f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[2], 0.8f, 0, -5, -30);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[2], 0.8f, 0, -5, -25);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[2], 0.8f, 0, -5, -20);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[2], 0.8f, 0, -5, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, randval, 17, 0, 0, 0, BulletSprites[2], colors[2], 0.8f, 0, -5, -10);
                break;

            case shootingPattern.HAHAHA:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 17, 0, 0, 0, BulletSprites[3], colors[0], 3.0f, 0, 0, -20);
                break;

            case shootingPattern.BIRDS:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[10], colors[0], 0.8f, 0, 15, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[10], colors[0], 0.8f, 0, -15, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[10], colors[0], 0.8f, 0, 0, 15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[10], colors[0], 0.8f, 0, 0, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[10], colors[0], 0.8f, 0, 15, 15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[10], colors[0], 0.8f, 0, -15, 15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[10], colors[0], 0.8f, 0, 15, -15);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[10], colors[0], 0.8f, 0, -15, -15);
                break;

            case shootingPattern.THRU_THE_WALLS:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 0, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 3, -2f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 0, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, -3, -2f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 0, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 3, -2f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 0, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, -3, -2f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 0, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, 3, -2f);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 0, 0, 0, 0, BulletSprites[0], colors[0], 2.0f, 0, -3, -2f);
                break;

            case shootingPattern.HUSTLE_BONES:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), 0, 0, 0, 0, BulletSprites[3], colors[0], Random.Range(1.0f, 2.0f), 0, 0, Random.Range(-20f, -8f));
                break;

            case shootingPattern.LIL_BOY:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 5, -5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, -5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, -5, -5);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 5, -5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, -5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, -5, -5);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 5, -5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, -5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, -5, -5);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, -10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, -10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, -10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 5, -5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, -10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, -5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, -10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, -5, -5);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, -10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, -10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, -10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, 5, -5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, -10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, -5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, -10, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, 2, -5, -5);
                break;

            case shootingPattern.DEATH_HEATED:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[1], 0.8f, 4, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[2], 0.8f, 4, 5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[2], 0.8f, 4, 5, -5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[2], 0.8f, 4, -5, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[2], 0.8f, 4, -5, -5);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[2], 0.8f, 4, 10, 10);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[2], 0.8f, 4, 10, -10);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[2], 0.8f, 4, -10, 10);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[2], 0.8f, 4, -10, -10);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[1], 0.8f, 4, 5, 10);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[1], 0.8f, 4, 10, -5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[1], 0.8f, 4, -5, 10);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[1], 0.8f, 4, -10, -5);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[1], 0.8f, 4, 10, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[1], 0.8f, 4, 5, -10);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[1], 0.8f, 4, -10, 5);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[9], colors[1], 0.8f, 4, -5, -10);
                break;

            case shootingPattern.SYSTEM_BLOWER:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, 0, 0, 0, BulletSprites[9], colors[0], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, 0, 0, 0, BulletSprites[9], colors[1], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, 0, 0, 0, BulletSprites[9], colors[2], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, 0, 0, 0, BulletSprites[9], colors[3], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                break;

            case shootingPattern.HACKER:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, 10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, -10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, -10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, -10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 10, -10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, 10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, -10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, -10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, -10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10, -10, 0, 0, 0, BulletSprites[9], colors[Random.Range(0, 3)], 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
                break;

            case shootingPattern.ARTIFICIAL_DEATH:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0, 0, BulletSprites[0], colors[Random.Range(0, 3)], 13.0f, -5, 0, 0);
                break;

           
             //boss 5
           

            case shootingPattern.ALL_MY_LIFE:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, Random.Range(-40f, 40f), 5f, 0, 0, 0, 0, BulletSprites[6], colors[0], 0.6f, 0, Random.Range(-18f,-6f), 0);
                break;

            case shootingPattern.CENTURIES_OF_DAMN:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[1], 1.0f, 0, 15, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[1], 1.0f, 0, -15, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[1], 1.0f, 15, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[1], 1.0f, -15, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[1], 1.0f, 15, 15, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[1], 1.0f, 15, -15, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[1], 1.0f, -15, 15, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[1], 1.0f, -15, -15, 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[2], 2.0f, 7, 14, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[2], 2.0f, 7, -14, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[2], 2.0f, 14, 7, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[2], 2.0f, -14, 7, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[2], 2.0f, -7, 14, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[2], 2.0f, -7, -14, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[2], 2.0f, 14, -7, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[8], colors[2], 2.0f, -14, -7, 0);


                break;

            case shootingPattern.GIVING_BAD_PEOPLE_GOOD_IDEAS:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10f, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5,11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, -10f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                break;

            case shootingPattern.TWO_HEAVENS:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -30f, -15f, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -30f, -15f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -30f, -15f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 30f, -15f, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 30f, -15f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 30f, -15f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                break;

            case shootingPattern.BEYOND_ALIVE:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -30f, 0, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -30f, 0, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -30f, 0, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 30f, 0, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 30f, 0, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 30f, 0, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -30f, -25f, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -30f, -25f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -30f, -25f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 30f, -25f, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 30f, -25f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 30f, -25f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                break;

            case shootingPattern.ON_GP:
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -20f, 0, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -20f, 0, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -20f, 0, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 20f, 0, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 20f, 0, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 20f, 0, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -20f, -25f, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -20f, -25f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -20f, -25f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 20f, -25f, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 20f, -25f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 20f, -25f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);


                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0, 0, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0f, -25f, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0f, -25f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 0f, -25f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 35f, -15f, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 35f, -15f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, 35f, -15f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -35f, -15f, -4f, 0, 0, 0, BulletSprites[0], colors[4], 3.0f, 0, 0, 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -35f, -15f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                bc.GetComponent<BulletController>().ShootExtra(BulletTypes[0], shotSpawn, -35f, -15f, 0, 0, 0, 0, BulletSprites[Random.Range(1, 10)], colors[Random.Range(5, 11)], Random.Range(0.5f, 1.0f), Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                break;
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
