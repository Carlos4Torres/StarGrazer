using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public EnemyMainPath mainController;

    [Header("Heath")]
    public float health;
    public float dmgPerBullet;


    public void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Bullet") && mainController.state != EnemyMainPath.combatState.IDLE)
            {
                health -= dmgPerBullet;
                //Destroy(other.gameObject.transform.parent);
                //Destroy(other.gameObject);
                Debug.Log("fuck");
                if (health <= 0)
                {
                    mainController.DestroyThis();
                }
            }
        
    }
}
