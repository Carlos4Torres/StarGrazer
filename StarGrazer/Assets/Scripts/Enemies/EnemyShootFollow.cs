using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootFollow : MonoBehaviour
{
    //IMPORTANT// Currently, this script on the EnemyBullet prefab only supports a public speed value of around 10 or higher, or the targetting stops working. My fried brain is having a hard time finding out why at the moment.


    
    //Speed of Bullet
    public float speed;
    //Damage to player per bullet
    public int damage = 40;

    //Datafields for targeting system
    private Transform player;
    private Vector3 target;

    void Start()
    {
        //Finding the player game object
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Set targetting values according to player position
        target = new Vector3(player.position.x, player.position.y,player.position.z);
    }

    
    void Update()
    {
        //Moving the bullet towards player's position
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //If the bullet reaches the player's position, it deletes itself
        if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z==target.z)
        {
            DestroyProjectile();
        }

        Destroy(gameObject, 10f);
    }
   
    private void OnTriggerEnter(Collider collision)
    {
        //Get the player health component when colliding with the player
        PlayerHealth health = collision.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.Demage(damage);
        }

        if (collision.CompareTag("Player"))
        {
            DestroyProjectile();
        }

       
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);

    }
}
