using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  /// Public DataFields
    public GameObject bulletPrefab;
    public Transform gameplayPlane;
    public Sprite sgShot;
    public float fireRate = .1f;
    public float shotSpeed = 50;
    public float bulletRadius = 0.5f;
    public static bool alive;

    PlayerControls controls;
    private bool shooting;
    private BulletController bulletController;


    [SerializeField]
    private int grazeCount;

    //Necessary for Input System
    void OnEnable()                                                                     
    {                                                                                   
        controls.Gameplay.Enable();                                                     
    }

    //Necessary for Input System
    void OnDisable()                                                                    
    {                                                                                   
        controls.Gameplay.Disable();                                                    
    }                                                                                   
                                                                                        
    private void Awake()                                                                
    {
        bulletController = GetComponent<BulletController>();

        controls = new PlayerControls();

        controls.Gameplay.PlayerFire.started += ctx => CanShoot();                                  // If fire pressed, call shoot function
        controls.Gameplay.PlayerFire.canceled += ctx => shooting = false;                           // If released, stop shooting
    }

    // Fires bullet every fireRate seconds while shooting is true;
    IEnumerator FireRate()                                                                     
    {                                                                                          
        yield return new WaitForSeconds(fireRate);     //There may be a better way to do this, but for now the different speed variables consist of transform.forward * speed which fires at the crosshair at a specific speed.    |                        
        if (shooting && alive)                                  //The gameplayPlane.forward * 20 ensures adds the forward speed to the bullet so that it actually goes at the crosshair instead of veering off.                             V
        {                                               
            bulletController.Shoot(transform, transform.rotation.x, transform.rotation.y, transform.rotation.z, sgShot, Color.green, 1, gameplayPlane.forward.x * 20 + transform.forward.x * shotSpeed, gameplayPlane.forward.y * 20 + transform.forward.y * shotSpeed, gameplayPlane.forward.z * 20 + transform.forward.z * shotSpeed, bulletRadius);
            shooting = true;
            StartCoroutine(FireRate());
        }
    }

    void CanShoot()
    {
        if (!shooting && alive)
        {
            bulletController.Shoot(transform, transform.rotation.x, transform.rotation.y, transform.rotation.z, sgShot, Color.green, 1, gameplayPlane.forward.x * 20 + transform.forward.x * shotSpeed, gameplayPlane.forward.y * 20 + transform.forward.y * shotSpeed, gameplayPlane.forward.z * 20 + transform.forward.z * shotSpeed, bulletRadius);
            shooting = true;
            StartCoroutine(FireRate());
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            grazeCount++;
        }
    }

}
