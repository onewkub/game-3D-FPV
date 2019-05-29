﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnermyScript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] wayPoints;
    private Transform PlayerLastPosition;
    public GameObject Player;
    private bool lastPosChange;
    int m_currIndex;
    private void Start()
    {
        navMeshAgent.SetDestination(wayPoints[0].position);
        lastPosChange = false;
    }

    void Update()
    {
        if (lastPosChange)
        {
            changeDest();
            lastPosChange = false;
        }
        else
        {
            checkDest();
        }
    }
    private void checkDest()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            //Debug.Log("new Dest");
            m_currIndex = (m_currIndex + 1) % wayPoints.Length;
            navMeshAgent.SetDestination(wayPoints[m_currIndex].position);
        }
    }
    private void changeDest()
    {
        navMeshAgent.SetDestination(PlayerLastPosition.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == Player.transform)
        {
            Debug.Log("It crash");
            //Destroy(Player);
        }
    }

    public void SetPos()
    {
        PlayerLastPosition = Player.transform;
        lastPosChange = true;
    }
}
