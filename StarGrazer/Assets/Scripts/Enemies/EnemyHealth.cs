using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public EnemyMainPath mainController;

    [Header("Heath")]
    public float health;
    public float dmgPerBullet; //Damage the enemy takes per shot. Ideally would attach this value to the bullet instead of the enemy


    public void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Bullet") && mainController.state != EnemyMainPath.combatState.IDLE)
            {
                health -= dmgPerBullet;
                Destroy(other.gameObject);
            if (health <= 0)
                {
                    mainController.DestroyThis();
                }
            }
        
    }
}
