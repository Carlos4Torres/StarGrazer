using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public EnemyMainPath mainController;

    [Header("Heath")]
    public float health;
    public float dmgPerBullet; //Damage the enemy takes per shot. Ideally would attach this value to the bullet instead of the enemy

    public AudioSource deathSound;

    public void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Bullet") && mainController.state != EnemyMainPath.combatState.IDLE)
            {
                health -= dmgPerBullet;

                if (health <= 0)
                {
                    deathSound.Play();
                    StartCoroutine(mainController.DestroyThis());
                    mainController.isDead = true;
                    var mesh = GetComponent<MeshRenderer>();
                    mesh.enabled = false;
                    var collider = GetComponent<BoxCollider>();
                    collider.enabled = false;
                }
            }
        
    }
}
