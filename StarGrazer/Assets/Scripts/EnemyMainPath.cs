﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    [Header("Escape Values")]
    public float escapeSpeed;
    public float timeUntilEscape;
    public float timeUntilDestroy = 5f;
    private float moveDampen = 0;

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

                localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, mainDollyScipt.m_Speed, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                if (mainDollyScipt.m_Speed - localDollyScript.m_Speed < 1)
                {
                    state = combatState.COMBAT;
                    moveDampen = 0;
                }
                break;

            case combatState.COMBAT:
                StartCoroutine(Escape());
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

    public void EnterCombat()
    {
            state = combatState.ENTRY;
            localDollyScript.m_Speed = escapeSpeed;
    }
}
