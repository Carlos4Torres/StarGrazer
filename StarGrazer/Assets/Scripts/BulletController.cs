using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //This script contains the function used to summon a bullet prefab with all of its specifications.
    //First, you would need to have your schedule script reference this one (public gameobject, drag schedule object in inspector, 
    //get component<'object with this script, let's call it bc')
    //then, you'd go bc.spawnbullet(arguments);

    public GameObject bulletprefab;

    void spawnbullet ()
    {
        GameObject inst = Instantiate(bulletprefab);
        
    }
        
        
        // Update is called once per frame
    void Update()
    {
        
    }
}
