using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnermyScript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] wayPoints;

    public GameObject Player;
    int m_currIndex;
    private void Start()
    {
        navMeshAgent.SetDestination(wayPoints[0].position);
    }

    void Update()
    {
        checkDest();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == Player.transform)
        {
            Debug.Log("It crash");
            Destroy(Player);
        }
    }
}
