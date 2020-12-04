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
            //L1G1();
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
        //this is a tri-shot just to get a good test going
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 0, sprite, color, .5f, 0, 0, 5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, 45, sprite, color, .5f, 0, 0, 5);
        bc.GetComponent<BulletController>().Shoot(shotSpawn, 0, 0, -45, sprite, color, .5f, 0, 0, 5);
    }



}