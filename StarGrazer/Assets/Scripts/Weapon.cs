using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  /// Public DataFields
   public GameObject bulletPrefab;
   public Transform firePosition;
    public Transform gameplayPlane;

    PlayerControls controls;
    private bool shooting;
    private Rigidbody rb;

    void OnEnable()                                                                     
    {                                                                                   
        controls.Gameplay.Enable();                                                     
    }                                                                                   
                                                                                        
    void OnDisable()                                                                    
    {                                                                                   
        controls.Gameplay.Disable();                                                    
    }                                                                                   
                                                                                        
    private void Awake()                                                                
    {                                                                                   
        controls = new PlayerControls();                                                
                                                                                        
        controls.Gameplay.PlayerFire.started += ctx => Shoot();                   
        controls.Gameplay.PlayerFire.canceled += ctx => shooting = false;

        rb = GetComponentInParent<Rigidbody>();
        print(rb);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Shoot();
        //}
    }

    IEnumerator FireRate()                                                                     
    {                                                                                          
        yield return new WaitForSeconds(.1f);                                                 
        if (shooting)                                                                          
            Shoot();                                                                           
    }

    void Shoot()
    {
        shooting = true;
        GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
        Rigidbody bulletrb = bullet.GetComponent<Rigidbody>();
        bulletrb.velocity += gameplayPlane.transform.forward * 20;
        bulletrb.angularVelocity += rb.angularVelocity;
        GetComponent<AudioSource>().Play();
        StartCoroutine("FireRate");
    }
}
