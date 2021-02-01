using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody rb;
    public int lifetime;

    [Header("Damage Values")]
    //Damage bullet does when hitting an enemy and hitting the player respectively
    //Need two differnt values since enemy and player use the same bulelt prefab
    public  int enemyDamage = 10;
    public const int playerDamage = 1;

    PlayerHealth heathScript;

    void Awake()
    {
        rb.velocity += transform.forward * speed;
    }

    private void Update()
    {
        Destroy(gameObject, lifetime);
    }

    //Damages player & destroys bullet
    public void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("PlayerHealth") && CompareTag("Bullet") == false)
      {
            //We should change this to intialize on start
            heathScript = other.GetComponent<PlayerHealth>();
            heathScript.Damage(playerDamage);
            Destroy(gameObject);
      }
     // else if (other.CompareTag("Enemy Model") && CompareTag("Bullet") == false)
     // {
     //       enemyHealth = other.GetComponent<EnemyHealth>();
     //       enemyHealth.Damage(enemyDamage);
     //       Destroy(gameObject);
     // }
    }
}
 