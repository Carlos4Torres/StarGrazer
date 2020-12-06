using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reversedirection : MonoBehaviour
{
    //the speed the bullet starts at
    public float startspeed;
    //the number decreased from the speed every frame until it starts moving into the negatives.
    public float speeddepletion;

    public int timer = 180;

    // Update is called once per frame
    void Update()
    {
        startspeed -= speeddepletion;
        transform.Translate(0, startspeed, 0);
        timer--;
        if (timer <= 0) { Destroy(gameObject); }
    }
}
