using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootFollow : MonoBehaviour
{
    //IMPORTANT// Currently, this script on the EnemyBullet prefab only supports a public speed value of around 10 or higher, or the targetting stops working. My fried brain is having a hard time finding out why at the moment.


    
    //Speed of Bullet
    public float speed;
    //Damage to player per bullet
    public int damage = 1;

    public float speed2 = 10;

    public GameObject self;

    //Datafields for targeting system
    private Transform player;
    private Vector3 target;

    public int mode;

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
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            //If the bullet reaches the player's position, it deletes itself
            if ((transform.position.x >= target.x - 2) && (transform.position.x <= target.x + 2) && (transform.position.y >= target.y - 2) && (transform.position.y <= target.y + 2) && (transform.position.z >= target.z - 2) && (transform.position.z <= target.z + 2))
            {
                //standard
                if (mode == 0) { Destroy(gameObject); }
                //bomb
                if (mode == 1)
                {
                  explode();
                }

                if (mode == 2) { speed2 -= 0.2f; }
            }
        }
    }
   
    private void OnTriggerEnter(Collider collision)
    {
        //Get the player health component when colliding with the player
        PlayerHealth health = collision.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.Damage(damage);
        }

            if ((collision.CompareTag("Player")) && (mode == 1))
            {
            explode();
            }
    }


    private void explode()
    {
        GameObject clone1 = Instantiate(self, transform.position, Quaternion.Euler(0, 0, 0));
        GameObject clone2 = Instantiate(self, transform.position, Quaternion.Euler(45, 0, 0));
        GameObject clone3 = Instantiate(self, transform.position, Quaternion.Euler(90, 0, 0));
        GameObject clone4 = Instantiate(self, transform.position, Quaternion.Euler(135, 0, 0));
        GameObject clone5 = Instantiate(self, transform.position, Quaternion.Euler(180, 0, 0));
        GameObject clone6 = Instantiate(self, transform.position, Quaternion.Euler(225, 0, 0));
        GameObject clone7 = Instantiate(self, transform.position, Quaternion.Euler(270, 0, 0));
        GameObject clone8 = Instantiate(self, transform.position, Quaternion.Euler(315, 0, 0));
        Destroy(gameObject);
    }
}
