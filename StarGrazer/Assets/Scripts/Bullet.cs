using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody rb;
    public int lifetime;
    
    PlayerHealth heathScript;

    // Start is called before the first frame update
    void Awake()
    {
        //rb.velocity = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z * speed);
        rb.velocity += transform.forward * speed;
    }

    private void Update()
    {
        //rb.velocity = Vector3.forward * Time.deltaTime * speed;
        Destroy(gameObject, lifetime);
    }


    //Damages player & destroys bullet
    public void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("PlayerHealth") && CompareTag("Bullet") == false)
      {
         heathScript = other.GetComponent<PlayerHealth>();
         heathScript.Damage();
         Destroy(gameObject);
      }
    }


}
 