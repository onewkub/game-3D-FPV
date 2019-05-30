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
    private bool chasePlayer = false;

    public float visibilityRange = 20f;
    public float nearRange = 0.5f;
    private void Start()
    {
        navMeshAgent.SetDestination(wayPoints[0].position);
        lastPosChange = false;
    }

    void Update()
    {
        if (lastPosChange)
        {
            ChangeDest();
            lastPosChange = false;
        }
        else
        {
            CheckDest();
        }

        // Player follow (use line cast for obstacle check)
        if(Physics.Linecast(transform.position, Player.transform.position, out RaycastHit hit))
        {
            Debug.DrawLine(transform.position, Player.transform.position);
            if (hit.transform.CompareTag("Player") && hit.distance < visibilityRange)
            {
                if (hit.distance < nearRange)
                {
                    // make enemy able to sense if player is very close
                    SetPos();
                    chasePlayer = true;
                }
                else if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position)) < 30)
                {
                    // if player in forward line of sight (30 deg angle)
                    SetPos();
                    chasePlayer = false;
                }
                
            }
        }
        
    }
    private void CheckDest()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            //Debug.Log("new Dest");
            if (chasePlayer)
            {
                chasePlayer = false;
                //TODO: make enemy wander toward rotation player last seen
            }
            else
            {
                m_currIndex = (m_currIndex + 1) % wayPoints.Length;
                navMeshAgent.SetDestination(wayPoints[m_currIndex].position);
            }
        }
    }
    private void ChangeDest()
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
