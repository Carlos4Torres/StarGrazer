using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnChildren : MonoBehaviour
{
    public void SetChildrenActive()
    {
        for (int j = 0; j < this.transform.childCount; j++)
        {
            this.transform.GetChild(j).gameObject.SetActive(true);
        }
    }
}
