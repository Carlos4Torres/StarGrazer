using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //This script contains the function used to summon a bullet prefab with all of its specifications.
    //First, you would need to have your schedule script reference this one (public gameobject, drag schedule object in inspector, 
    //get component<'object with this script, let's call it bc')
    //then, you'd go bc.spawnbullet(arguments);

    public GameObject bulletPrefab;



    // Summon a bullet prefab with all of it's specifications
    public void Shoot(Transform spawnPosition, float rotationX, float rotationY, float rotationZ, Sprite sprite, Color color, float scale, float speedX, float speedY, float speedZ, float collliderRadius)
    {
   
        //shooting = true;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition.position, Quaternion.Euler(rotationX, rotationY, rotationZ));
        SpriteRenderer sr = bullet.GetComponentInChildren<SpriteRenderer>();
        sr.sprite = sprite;
        GameObject child = bullet.transform.GetChild(0).gameObject;
        child.transform.LookAt(Camera.main.transform.position, -Vector3.up);
        sr.color = color;
        bullet.transform.localScale = new Vector3(scale, scale, scale);                                                                 
        Rigidbody bulletrb = bullet.GetComponent<Rigidbody>();
        SphereCollider bulletColl = bullet.GetComponent<SphereCollider>();
        bulletrb.velocity = new Vector3(speedX, speedY, speedZ);
        bulletColl.radius = collliderRadius;
        //GetComponent<AudioSource>().Play();
    }

    public void ShootExtra(GameObject prefab, Transform spawnPosition, float posX, float posY, float posZ, float rotationX, float rotationY, float rotationZ, Sprite sprite, Color color, float scale, float speedX, float speedY, float speedZ, float collliderRadius)
    {

        //shooting = true;
        GameObject bullet = Instantiate(prefab, spawnPosition.position, Quaternion.Euler(rotationX, rotationY, rotationZ));
        bullet.transform.position = new Vector3(spawnPosition.position.x + posX, spawnPosition.position.y + posY, spawnPosition.position.z + posZ);
        bullet.transform.localScale = new Vector3(scale, scale, scale);
        Rigidbody bulletrb = bullet.GetComponent<Rigidbody>();
        bulletrb.velocity = new Vector3(speedX, speedY, speedZ);
        SpriteRenderer sr = bullet.GetComponentInChildren<SpriteRenderer>();
        sr.sprite = sprite;
        sr.color = color;
        GameObject child = bullet.transform.GetChild(0).gameObject;
        SphereCollider bulletColl = bullet.GetComponent<SphereCollider>();
        child.transform.LookAt(Camera.main.transform.position, -Vector3.up);
        bulletColl.radius = collliderRadius;

        //GetComponent<AudioSource>().Play();
    }


    //specific action bullets



}
