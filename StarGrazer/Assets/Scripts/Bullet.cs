using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public float speed=20f;
    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z * speed);
        rb.velocity = transform.forward * speed;
    }

    private void Update()
    {
        //rb.velocity = Vector3.forward * Time.deltaTime * speed;
        Destroy(gameObject, 2);
    }
    
    
}
