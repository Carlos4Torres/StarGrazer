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
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots);
        }

    }

    private void Shoot()
    {
        if(isDead == false)
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            laserSound.Play();
        }

    }

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

}
