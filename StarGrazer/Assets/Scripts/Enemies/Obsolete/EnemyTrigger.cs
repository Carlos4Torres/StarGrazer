using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{


    public EnemyMainPath[] mainEnemies;
    public EnemyDynamicPath[] dynmEnemies;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && mainEnemies != null)
        {
            foreach(EnemyMainPath enemy in mainEnemies)
            {
                enemy.Enter();
            }
        }

        if (other.gameObject.CompareTag("Player") && dynmEnemies != null)
        {
            foreach(EnemyDynamicPath enemy in dynmEnemies)
            {
                enemy.EnterCombat();
            }
        }
    }
}
