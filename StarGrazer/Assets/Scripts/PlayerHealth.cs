using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //For archiving purposes, this script must be accompanied by a box collider on CubeGrazer in order to work properly with interaction with enemy bullets.

    //Public variable for health value
    public int health;

    //Demage methed will reduce the health of player
    public void Demage(int demage)
    {
        //health value reduce when player hit
        health -= demage;

        //if health zero than player destroy or kill
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
