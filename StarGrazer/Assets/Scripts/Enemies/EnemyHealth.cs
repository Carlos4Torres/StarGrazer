using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Variables")]
    public EnemyMainPath mainController;
    public AudioSource deathSound;
    

    private float health = 50;


    public void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Bullet") && mainController.state != EnemyMainPath.combatState.IDLE)
            {
                Bullet bulletScript = other.GetComponent<Bullet>();
                health -= bulletScript.enemyDamage;
  
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

  //public void Damage(int damageValue)
  //{
  //    if (mainController.state != EnemyMainPath.combatState.IDLE)
  //    {
  //        health -= damageValue;
  //        Debug.Log(health);
  //        if (health <= 0)
  //        {
  //            deathSound.Play();
  //            StartCoroutine(mainController.DestroyThis());
  //            mainController.isDead = true;
  //            var mesh = GetComponent<MeshRenderer>();
  //            mesh.enabled = false;
  //            var collider = GetComponent<BoxCollider>();
  //            collider.enabled = false;
  //        }
  //    }
  //}

}
