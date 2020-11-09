using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyDynamicPath : MonoBehaviour
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

    [Header("Scripts and Components")]
    public CinemachineSmoothPath entryPath;
    public CinemachineSmoothPath combatPath;
    public CinemachineSmoothPath escapePath;

    void Start()
    {

        localDollyScript = GetComponent<CinemachineDollyCart>();

        state = combatState.IDLE;
        localDollyScript.m_Speed = 0;
        localDollyScript.m_Path = entryPath;
    }

    void Update()
    {
        switch (state)
        {
            case combatState.ENTRY:

                localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, mainDollyScipt.m_Speed, moveDampen);
                moveDampen += 0.007f * Time.deltaTime; 
                if (entryPath.PathLength - localDollyScript.m_Position < 1)
                {
                    state = combatState.COMBAT;
                    localDollyScript.m_Speed = 0;
                    localDollyScript.m_Path = combatPath;
                    localDollyScript.m_Position = 0;
                    transform.GetChild(0).Rotate(0, 180, 0);
                    
                    moveDampen = 0;
                }
                break;

            case combatState.COMBAT:
                localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, mainDollyScipt.m_Speed, moveDampen);
                moveDampen += 0.007f * Time.deltaTime;
                StartCoroutine(TimeToEscape());
                break;

            case combatState.ESCAPE:
                if(combatPath != escapePath)
                {
                    localDollyScript.m_Path = escapePath;
                }
                localDollyScript.m_Path = escapePath;
                localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, escapeSpeed, moveDampen);
                moveDampen += 0.01f * Time.deltaTime;
                break;
        }
        //Debug.Log(localDollyScript.m_Speed);
    }

    IEnumerator TimeToEscape()
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
