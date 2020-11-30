using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBossFight : MonoBehaviour
{
    [Header("Objects")]
    public CinemachineDollyCart Player;
    public CinemachineDollyCart Boss;
    public CinemachineDollyCart TeleportPoint;

    private float Distance;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
                    Distance = Boss.m_Position - Player.m_Position;
                    Player.m_Position = TeleportPoint.m_Position;
                    Boss.m_Position = TeleportPoint.m_Position + Distance;
        }
    }
}
