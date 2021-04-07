using Cinemachine;
using System.Collections;
using UnityEngine;

public class EnemyMainPath : MonoBehaviour
{

    public enum combatState
    {
        IDLE,
        ENTRY,
        COMBAT,
        ESCAPE,
    }

    public combatState state;

    [Header("Shooting")]
    public float timeBetweenShots;
    public float initialShotDelay;
    public float shotSpeed = 25;
    public GameObject shot;
    public Transform shotSpawn;

    [Header("Escape Values")]
    public float escapeSpeed;
    public float timeUntilEscape;
    public float timeUntilDestroy = 5f;
    private float moveDampen = 0;

    public AudioSource laserSound;
    public bool isDead = false;



    [Header("Scripts and Components")]
    public CinemachineDollyCart mainDollyScipt; //Using this just to match speed
    private CinemachineDollyCart localDollyScript;
    private EnemyHealth enemyHealth;

    private float spawnPosition;
    public float rotationX; float rotationY; float rotationZ;
    public Sprite sprite;
    public Color color;


    //bullet controller variable that gets called
    public BulletController bc;

    //using the Invoke function, we can publically decide what attack the enemy will use based on a string to call the respective action script.
    public string attacktype;
    private int swap = 0;

    void Start()
    {
        enemyHealth = GetComponentInChildren<EnemyHealth>();
        localDollyScript = GetComponent<CinemachineDollyCart>();

        state = combatState.IDLE;
        localDollyScript.m_Speed = 0;
        spawnPosition = localDollyScript.m_Position;
    }

    void Update()
    {
        switch (state)
        {
            case combatState.ENTRY:

                localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, 5, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                if (localDollyScript.m_Position - mainDollyScipt.m_Position < 20)
                {
                    StartCombat();
                }
                break;

            case combatState.COMBAT:

                break;

            case combatState.ESCAPE:
                localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, escapeSpeed, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                break;
        }
    }

    IEnumerator Escape()
    {
        yield return new WaitForSeconds(timeUntilEscape);
        state = combatState.ESCAPE;

        //Destroy(this.gameObject, timeUntilDestroy);
        yield return new WaitForSeconds(5);
        if(state == combatState.ESCAPE)
            this.gameObject.SetActive(false);
    }

    public void Enter()
    {
        state = combatState.ENTRY;
        localDollyScript.m_Speed = escapeSpeed;
    }

    public void ResetEnemy()
    {
        isDead = false;
        state = combatState.IDLE;
        localDollyScript.m_Speed = 0;
        localDollyScript.m_Position = spawnPosition;
        enemyHealth.Reset();
        StopCoroutine(Escape());
    }

    public void StartCombat()
    {
        state = combatState.COMBAT;
        localDollyScript.m_Speed = mainDollyScipt.m_Speed;
        moveDampen = 0;

        StartCoroutine(CombatControl());
        StartCoroutine(Escape());

    }

    public void OnTriggerEnter(Collider other)
    {
        if (state == combatState.IDLE && other.gameObject.CompareTag("Player"))
        {
            Enter();
            var boxCollider = GetComponent<BoxCollider>();
            //boxCollider.enabled = false;
        }
    }



    IEnumerator CombatControl()
    {
        yield return new WaitForSeconds(initialShotDelay);
        while (state == combatState.COMBAT)
        {
            Shooted();
            yield return new WaitForSeconds(timeBetweenShots);
        }

    }

    private void Shooted()
    {
        if (isDead == false)
        {
            //executes the attack function in the bullet controller
            Invoke(attacktype, 0);
            laserSound.Play();
        }

    }


    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(2);
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }


    //---------------------------------------------------------------------------------------------------------------------------------------------------


    //very very long list of enemy attacks because there's literally nowhere else I can put it that isnt a huge pain in the neck

    private void L1G1()
    {
        //Double Tri-Shot
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, -10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 7, -7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -7, -7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, -4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 7, -1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -7, -1);
        return;
    }

    private void L1G2()
    {
        //Lines
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -20);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -16);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -12);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -8);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -4);
        return;
    }

    private void L1G3()
    {
        //combination

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 0, -20);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 0, -16);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 0, -12);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 0, -8);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 0, -4);
        return;
    }


    private void L1G4()
    {
        if(swap == 0)
        {
            Invoke("L1G41", 0);
            swap = 1;
        }
        else
        Invoke("L1G42", 1);
    }

    private void L1G41()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, 0, 0, -10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, 0, 0, 10);
    }

    private void L1G42()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, -10, 0, -10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, 10, 0, 10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, -10, 0, 10);
    }

    private void L1G5()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, 25);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, -25);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, 40);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, -40);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, 25);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, -25);
      // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, 40);
      // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, -40);

    }

    private void L1G6()
    {
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 10, 0);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, -10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 10, 10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, -10);
      //  bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 10, -10);
      //  bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 10);
    }

    private void L1G7()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, -20, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, -16, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, -12, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, -8, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, -4, 0);
    }

    private void L1G8()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, 5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, 5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -5);
    }

    private void L1G9()
    {
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 0);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, 5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, 5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, -7);
      //  bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -7, 10, 0);
      //  bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 7, 10, 0);
    }

    private void L1G10()
    {
        if (swap == 0)
        {
          //  bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 0);
          //  bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, 5);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -5);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, 5);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -5);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 7);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, -7);
           // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -7, 10, 0);
           // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 7, 10, 0);

            swap = 1;
        }
        else
        {
        //    bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 10, 10, 10);
         //   bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -10, 10, -10);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -10, 10, 10);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 10, 10, -10);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 10);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, -10);
        //    bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -10, 10, 0);
         //   bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 10, 10, 0);
        }

    }


    //-----------------------------------------------------------------------------------------------------------------------------------------

    private void L2G1()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 5, -10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, -5, -10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 5, -10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, -5, -10);
    }

    private void L2G2()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, -10, -7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, -10, -7);
    }

    private void L2G3()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -3);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, -10, -7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -9);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, -10, -11);
    }

    private void L2G4()
    {
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -7);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -7);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, -10, -7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, -10, -7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 10, 13, -5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -10, -13, -5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -10, 13, -5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 10, -13, -5);
    }

    private void L2G5()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 4, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 3, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 2, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 1, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, 0);

    }

    private void L2G6()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 4, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 3, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 2, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 1, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, 0);

    }

    private void L2G7()
    {
       //c.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 5, 0, 0);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 4, 0, 0);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 3, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 2, 0, 0);
      bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 1, 0, 0);
      bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);

      // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 2, 4, 0);
      // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 2, -4, 0);
       bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 4, 12, 0);
       bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 4, -12, 0);


    }

    private void L2G8()
    {
        //bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, -7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, -7, 0);
        return;
    }

    private void L2G9()
    {
      //  bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 3, 0);
      //  bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, -3, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, -7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 3, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, -3, 0);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, 7, 0);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, -7, 0);
        return;
    }

    private void L2G10()
    {
        if(swap == 0)
        {
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 0, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 3, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, -3, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, 10, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, -10, 0);
            swap = 1;
        }
        else
        {
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 0, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 3, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, -3, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, 10, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, -10, 0);
            swap = 0;
        }

       
        return;
    }

    private void L2G11()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 5, -10, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -5, -10, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 15, -2, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -15, -2, 0);
        return;
    }

    private void L2G12()
    {
        if(swap == 0 )
        {
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -4, -3, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 4, -3, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -8, -1, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 8, -1, 0);
            
            swap = 1;
        }
        else
        {

            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 12, -3, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -12, -3, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -16, -1, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -16, -1, 0);
            swap = 0;
        }

        return;
    }

    private void L2G13()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -8, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -6, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -2, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
        return;
    }

    private void L2G14()
    {

        if (swap == 0)
        {
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 5, -10, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -5, -10, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 15, -2, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -15, -2, 0);

            swap = 1;
        }
        else
        {
          // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 0, -1, 0);
          // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -4, -3, 0);
          // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 4, -3, 0);
          // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -8, -1, 0);
          // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 8, -1, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 12, -3, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -12, -3, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -16, -1, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -16, -1, 0);
        }



        return;
    }

    private void L3G1()
    {
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 2);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 6, 4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 8, -4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 7, -12);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 9, 12);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 15, -6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 15, 6);
        return;
    }

    private void L3G2()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, -4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -2, -8);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -3, -12);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, -16);
        return;
    }

    private void L3G2_1()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, 4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -2, 8);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -3, 12);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, 16);
        return;
    }

    private void L3G3()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, -1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -8, -2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -12, -3);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -16, -4);
        return;
    }

    private void L3G3_1()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, 1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -8, 2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -12, 3);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -16, 4);
        return;
    }

    private void L3G4()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, -5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -8, -4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -6, -3);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, -2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -2, -1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 2, 1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 4, 2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 6, 3);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 8, 4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 10, 3);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 12, 2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 14, 1);
        return;
    }

    private void L3G5()
    {
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, 0, 10);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, 6, 0);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -6, 0);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, 0, -10);
       bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, 6, 10);
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -6, -10);
      //  bc.Shoot(shotSpawn, 0, 0, -5, sprite, color, 0.8f, 0, 6, -10);
       bc.Shoot(shotSpawn, 0, 0, -5, sprite, color, 0.8f, 0, -6, 10);

        return;
    }

    private void L3G6()
    {
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 10, 5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 5);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 5, -2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, -2);
        return;
    }

    private void L3G7()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 10, 10);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 10);
      // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 7, 5);
      // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -7, 5);
      //  bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 4, -2);
      //  bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, -2);

        return;
    }

    private void L3G8()
    {
       // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, -4);
      // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 4, 2);
      // bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, 2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 7, 7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -7, 7);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 10, 12);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 12);

        return;
    }

    private void L3G9()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 20.0f, 0, 0, 0);

        return;
    }

    private void L4G1()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 1, 3);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 2, 6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 3, 9);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 4, 12);

        return;
    }

    private void L4G2()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, 3);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -2, 6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -3, 9);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, 12);

        return;
    }

    private void L4G3()
    {
        if(swap == 0)
        {

            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 1, 1.5f);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 2, 3);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 3, 4.5f);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 4, 6);
            swap = 1;
        }
        else
        {
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, 1.5f);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -2, 3);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -3, 4.5f);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, 6);

            swap = 0;
        }



        return;
    }

    private void L4G4()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, -7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, -7, 0);
        return;
    }

    private void L4G5()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -20, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -16, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -12, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -8, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -4, 0, 0);
        return;
    }

    private void L4G6()
    {
        if(swap == 0)
        {
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -7, 7, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -7, -7, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -1, 7, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -1, -7, 0);

            swap = 1;
        }
        else
        {
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -20, 0, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -16, 0, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -12, 0, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -8, 0, 0);
            bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -4, 0, 0);

            swap = 0;
        }
        


        return;
    }


    private void L5G1()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -5, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -4, -6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -4, 6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -2, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, 0, 12);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, 0, -12);

    }

    private void L5G2()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -8, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -7, -6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -7, 6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -5, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -3, 12);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -3, -12);
    }

    private void L5G3()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -2, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -4, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -6, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -8, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -10, 0);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -2, -2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -4, -4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -6, -6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -8, -8);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -10, -10);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -2, 2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -4, 4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -6, 6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -8, 8);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -10, 10);

    }

    private void L5G4()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -2, -1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -4, -1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -6, -1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -8, -1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -10, -1);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -2, 1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -4, 1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -6, 1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -8, 1);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -10, 1);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -3, -2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -5, -4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -7, -6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -9, -8);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -11, -10);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -3, 2);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -5, 4);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -7, 6);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -9, 8);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.8f, 0, -11, 10);

    }


    private void L5G5()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, -7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, -7, 0);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -5, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -3.5f, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -3.5f, -7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -2, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -0.5f, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -0.5f, -7, 0);
        return;
    }

    private void L5G6()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -20, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -16, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -12, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -8, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -4, 0, 0);
        return;
    }

    private void L5G7()
    {
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -7, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -7, -7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -1, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -1, -7, 0);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -20, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -16, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -12, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -8, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -4, 0, 0);
        return;
    }

    private void L5G8()
    {
        //combination

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -7, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -7, -7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -1, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -1, -7, 0);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -20, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -16, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -12, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -8, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -4, 0, 0);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -20, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -16, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -12, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -8, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -4, 0, 0);
        return;
    }

    private void L5G9()
    {
        //combination

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -7, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -7, -7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -1, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -1, -7, 0);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -20, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -16, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -12, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -8, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, -4, 0, 0);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -20, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -16, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -12, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -8, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -4, 0, 0);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, -7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, -7, 0);

        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -5, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -3.5f, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -3.5f, -7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -2, 0, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -0.5f, 7, 0);
        bc.Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -0.5f, -7, 0);
        return;
    }

    private void L5G10()
    {
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));
        bc.Shoot(shotSpawn, Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0, sprite, color, 0.8f, Random.Range(-25f, 25f), Random.Range(-25f, 25f), Random.Range(-25f, 25f));

        return;
    }
}



    