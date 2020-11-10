using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject enemyBullet;

    void Start()
    { 
        timeBtwShots = startTimeBtwShots;
    }

    
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= 1;
        }
    }
}
