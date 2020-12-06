using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{


    private BossController bosscontrollerscript;


    public void Start()
    {
        bosscontrollerscript = GetComponent<BossController>();
    }

    //checks whether the phase has changed every frame
    private void Update()
    {

    }




}
