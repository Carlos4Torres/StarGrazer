using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public enum shootingPattern
    {
        DIAGONAL,
        WALL,
        STAGGERED,
    }
    
    [Header("Shooting")]
    private int loopsUntilPatternChange;
    public int changePatterEvery;
    public float timeBetweenShots;
    public float initialShotDelay;
    public float shotSpeed = 25;

    [Header("Components")]
    public GameObject shot;
    public Transform[] shotSpawn;
    public shootingPattern pattern;

    public void Start()
    {
        loopsUntilPatternChange = changePatterEvery;
    }
    public IEnumerator Shooting()
    {
        switch (pattern)
        {
            case shootingPattern.DIAGONAL:
                foreach (Transform location in shotSpawn)
                {
                    Instantiate(shot, location.position, location.rotation);
                    yield return new WaitForSeconds(timeBetweenShots);
                }
                break;


            case shootingPattern.WALL:
                foreach (Transform location in shotSpawn)
                {
                    Instantiate(shot, location.position, location.rotation);

                }
                yield return new WaitForSeconds(timeBetweenShots);
                break;


           // case shootingPattern.STAGGERED:
           //     foreach (Transform location in shotSpawn)
           //     {
           //         Instantiate(shot, location.position, location.rotation);
           //         location.
           //     }
           //     yield return new WaitForSeconds(timeBetweenShots);
           //     break;
        }

        loopsUntilPatternChange--;
        if(loopsUntilPatternChange == 0)
        {
            loopsUntilPatternChange = changePatterEvery;
            if (pattern == shootingPattern.DIAGONAL) pattern = shootingPattern.STAGGERED;
            else if (pattern == shootingPattern.STAGGERED) pattern = shootingPattern.DIAGONAL;
        }

    }

    public void Shoot()
    {
        StartCoroutine(Shooting());
    }
}
