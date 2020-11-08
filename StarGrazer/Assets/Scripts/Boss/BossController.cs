using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossController : MonoBehaviour
{

    public enum combatState
    {
        IDLE,
        ENTRY,
        COMBAT,
    }



    [Header("Shooting")]
    public float initialShotDelay;
    public float health;
    public float dmgPerBullet;

    public combatState state;


    [Header("Movement Values")]
    public float MoveSpeed;
    private float moveDampen = 0;



    [Header("Scripts and Components")]
    public CinemachineDollyCart mainDollyScipt; //Using this just to match speed
    private CinemachineDollyCart localDollyScript;
    private BossShooting bossShootingScript;

    void Start()
    {

        localDollyScript = GetComponent<CinemachineDollyCart>();
        bossShootingScript = GetComponent<BossShooting>();

        state = combatState.IDLE;
        localDollyScript.m_Speed = 0;
    }

    void Update()
    {
        switch (state)
        {
            case combatState.ENTRY:

                localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, mainDollyScipt.m_Speed, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                if (localDollyScript.m_Position - mainDollyScipt.m_Position < 40)
                {
                    StartCombat();
                }
                break;

            case combatState.COMBAT:
                break;
        }
    }

    public void Entry()
    {
        state = combatState.ENTRY;
        localDollyScript.m_Speed = MoveSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == combatState.IDLE)
            {
                if (localDollyScript.m_Position - mainDollyScipt.m_Position > 40)
                    MoveSpeed += ((localDollyScript.m_Position - mainDollyScipt.m_Position) * -.75f);

                var collider = this.GetComponent<BoxCollider>();
                collider.enabled = false;

                Entry();
            }
        }

        if (other.CompareTag("Bullet"))
        {
            if (state == combatState.COMBAT)
            {
                health -= dmgPerBullet;
                Destroy(other.gameObject);
            }
        }
    }

    public void StartCombat()
    {
        state = combatState.COMBAT;
        localDollyScript.m_Speed = mainDollyScipt.m_Speed;
        moveDampen = 0;

        StartCoroutine(CombatControl());
    }

    IEnumerator CombatControl()
    {
        yield return new WaitForSeconds(initialShotDelay);
        while (state == combatState.COMBAT)
        {
            StartCoroutine(bossShootingScript.Shooting());
            yield return new WaitForSeconds(bossShootingScript.timeBetweenShots * bossShootingScript.shotSpawn.Length + 1);
        }

    }
}
