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

    public Transform spawnPosition;
    public float rotationX; float rotationY; float rotationZ;
    public Sprite sprite;
    public Color color;


    //bullet controller variable that gets called
    public GameObject bc;

    //using the Invoke function, we can publically decide what attack the enemy will use based on a string to call the respective action script.
    public string attacktype;


    void Start()
    {

        localDollyScript = GetComponent<CinemachineDollyCart>();

        state = combatState.IDLE;
        localDollyScript.m_Speed = 0;
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

        Destroy(this.gameObject, timeUntilDestroy);
    }

    public void Enter()
    {
        state = combatState.ENTRY;
        localDollyScript.m_Speed = escapeSpeed;
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
        if(state == combatState.IDLE && other.gameObject.CompareTag("Player"))
        {
            Enter();
            var boxCollider = GetComponent<BoxCollider>();
            boxCollider.enabled = false;
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
        if(isDead == false)
        {
            //executes the attack function in the bullet controller
            Invoke(attacktype, 0);
            laserSound.Play();
        }

    }


    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }


    //---------------------------------------------------------------------------------------------------------------------------------------------------



    //very very long list of enemy attacks because there's literally nowhere else I can put it that isnt a huge pain in the neck

    private void L1G1()
    {
        //Double Tri-Shot
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 7, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -7, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, -4);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 7, -1);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -7, -1);
        return;
    }

    private void L1G2()
    {
        //Lines
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -20);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -16);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -12);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -8);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -4);
        return;
    }

    private void L1G3()
    {
        //combination

        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 7, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, -7, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 7, -1);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, -7, -1);
    
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 0, -20);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 0, -16);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 0, -12);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 0, -8);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.8f, 0, 0, -4);
        return;
    }


    private void L1G4 ()
    {
        Invoke("L1G41", 0);
        Invoke("L1G42", 1);
    }

            private void L1G41 ()
            {
                bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, 0, 0, -10);
                bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, 0, 0, 10);
                bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, -10, 0, 0);
                bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, 10, 0, 0);
            }

            private void L1G42()
            {
                bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, -10, 0, -10);
                bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, 10, 0, 10);
                bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, -10, 0, 10);
                bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 0.7f, 10, 0, -10);
            }

    private void L1G5 ()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, 25);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, -25);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, 40);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -5, -40);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, 25);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, -25);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, 40);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -1, -40);

        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 15, -60);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 20, -60);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 25, -60);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 15, 60);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 20, 60);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 25, 60);


    }

    private void L1G6 ()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 10, 10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 10, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 10);
    }

    private void L1G7()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, -20, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, -16, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, -12, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, -8, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, -4, 0);
    }

    private void L1G8()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, 5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, 5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -5);
    }

    private void L1G9()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, 5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, 5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -7, 10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 7, 10, 0);
    }

    private void L1G10()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, 5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, 5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -7, 10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 7, 10, 0);

        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 10, 10, 10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -10, 10, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -10, 10, 10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 10, 10, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, 10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 10, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -10, 10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 10, 10, 0);
    }


//-----------------------------------------------------------------------------------------------------------------------------------------

    private void L2G1()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 5, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, -5, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 5, -10);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, -5, -10);
    }

    private void L2G2()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, -10, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, -10, -7);
    }

    private void L2G3()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -3);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, -10, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -9);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, -10, -11);
    }

    private void L2G4()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 10, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, -10, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -5, 10, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, -10, -7);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 10, 13, -5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -10, -13, -5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, -10, 13, -5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 10, -13, -5);
    }

    private void L2G5()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 4, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 3, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 2, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 1, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, 0);

    }

    private void L2G6()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 5, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 4, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 3, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 2, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 1, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 0, 0, 0);

        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 3, 8, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 3.0f, 3, -8, 0);

    }

    private void L2G7()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 5, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 4, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 3, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 2, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 1, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);

        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 2, 4, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 2, -4, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 4, 12, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 4, -12, 0);
        

    }

    private void L2G8()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, 7, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, -7, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, 7, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, -7, 0);
        return;
    }

    private void L2G9()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, 7, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, -7, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, 7, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, -7, 0);
        return;
    }

    private void L2G10()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, 3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -10, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, 10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -7, -10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 0, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, 3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -4, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, 10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -1, -10, 0);
        return;
    }

    private void L2G11()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 5, -10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -5, -10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -2, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 15, -2, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -15, -2, 0);
        return;
    }

    private void L2G12()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 0, -1, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -4, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 4, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -8, -1, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 8, -1, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 12, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -12, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -16, -1, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -16, -1, 0);
        return;
    }

    private void L2G13()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -8, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -6, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -4, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -2, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, 0, 0);
    }

    private void L2G14()
    {
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 5, -10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -5, -10, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 0, -2, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, 15, -2, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 2.0f, -15, -2, 0);

        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 0, -1, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -4, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 4, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -8, -1, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 8, -1, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, 12, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -12, -3, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -16, -1, 0);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, 1.0f, -16, -1, 0);
        return;
    }



}