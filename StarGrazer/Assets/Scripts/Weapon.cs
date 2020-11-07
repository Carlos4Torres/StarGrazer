using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  /// Public DataFields
   public GameObject bulletPrefab;
   public Transform firePosition;

    PlayerControls controls;
    private bool shooting;

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
        Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
        StartCoroutine("FireRate");
    }
}
