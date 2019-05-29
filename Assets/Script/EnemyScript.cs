using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] wayPoints;
    private Transform PlayerLastPosition;
    public GameObject Player;
    private bool lastPosChange;
    private int m_currIndex;

    public float visibilityRange = 20f;
    public Transform debugPoint;
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

        // Player follow
        if(Physics.Linecast(transform.position, Player.transform.position, out RaycastHit hit))
        {
            Debug.DrawLine(transform.position, Player.transform.position);
            if (hit.transform.CompareTag("Player") && hit.distance < visibilityRange)
            {
                SetPos();
            }
        }
    }
    private void checkDest()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Debug.Log("new Dest");
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
        debugPoint.position = PlayerLastPosition.position;
    }
}
