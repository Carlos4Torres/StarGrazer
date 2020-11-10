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
    public Transform spawnPosition;
    public Space relativeTo;
    public float xSpeed, ySpeed, zSpeed, xRotation, yRotation, zRotation, scale;
    public Sprite sprite;
    public Color color;

    PlayerControls controls;
    bool shooting = false;
    bool canShoot = true;

    void OnEnable()                                                                                    // {
    {                                                                                                  //
        controls.Gameplay.Enable();                                                                    //
    }                                                                                                  //
                                                                                                       //
    void OnDisable()                                                                                   //
    {                                                                                                  //
        controls.Gameplay.Disable();                                                                   //
    }                                                                                                  //
                                                                                                       //
    private void Awake()                                                                               //
    {                                                                                                  //
        controls = new PlayerControls();                                                               //
                                                                                                       //
        controls.Gameplay.PlayerFire.started += ctx => spawnbullet();                                  //   This should all probably be in the shooting script
        controls.Gameplay.PlayerFire.canceled += ctx => shooting = false;                              //       
    }                                                                                                  //
                                                                                                       //
    void spawnbullet ()                                                                                //
    {                                                                                                  //
        shooting = true;                                                                               //
        GameObject inst = Instantiate(bulletprefab, spawnPosition);                                    //
        inst.transform.localScale = new Vector3(scale, scale, scale);                                  //
        inst.transform.eulerAngles = new Vector3(xRotation, yRotation, zRotation);                     //
        print("shooted");                                                                              //
        StartCoroutine("FireSpeed");                                                                   //
    }                                                                                                  //
                                                                                                       //
    IEnumerator FireRate()                                                                             //
    {                                                                                                  //
        yield return new WaitForSeconds(.03f);                                                         //
        if(shooting)                                                                                   //
            spawnbullet();                                                                             //
    }                                                                                                  // }
        
        // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(xSpeed, ySpeed, zSpeed), relativeTo);
    }
}
