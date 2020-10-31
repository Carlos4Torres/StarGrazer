using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{

    public enum combatState
    {
        IDLE,
        COMBAT,
        ESCAPE,
    }

    public combatState state;

    [Header("Escape Values")]
    public float timeUntilEscape;
    public float escapeSpeed;

    [Header("Scripts and Components")]
    public CinemachineDollyCart mainDollyScipt;
    private CinemachineDollyCart localDollyScript;
    private BoxCollider boxCollider;

    void Start()
    {

        localDollyScript = GetComponent<CinemachineDollyCart>();
        boxCollider = GetComponent<BoxCollider>();
        
        state = combatState.IDLE;
        localDollyScript.m_Speed = 0;
    }

    void Update()
    {
        switch(state)
        {
            case combatState.COMBAT:
                StartCoroutine(Escape());
                break;
            case combatState.ESCAPE:
              // Trying to make it so it scales up the speed instead of just instant
              //  localDollyScript.m_Speed = Mathf.Lerp(localDollyScript.m_Speed, escapeSpeed, 20f);
              //  Debug.Log(localDollyScript.m_Speed);
                break;
        }
    }

    IEnumerator Escape()
    {
        yield return new WaitForSeconds(timeUntilEscape);
       
        state = combatState.ESCAPE;
        localDollyScript.m_Speed = escapeSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            localDollyScript.m_Speed = mainDollyScipt.m_Speed;
            state = combatState.COMBAT;
            boxCollider.enabled = false;
        }
    }
}
