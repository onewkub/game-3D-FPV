using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Header("Nav mesh things")]
    public NavMeshAgent navMeshAgent;
    public Transform[] wayPoints;
    private Transform PlayerLastPosition;
    public GameObject Player;
    private Rigidbody playerBody;
    private bool lastPosChange;
    private int m_currIndex;
    
    [Header("Enemy Detection System")]
    [Tooltip("Increse the detection speed when player stand still")]
    [Range(0f, 360f)]
    public float lineOfSight = 30f;
    [Tooltip("Threshold for player speed uses in multiplying enemy visibility")]
    public float quickThreshold = 2.5f;
    [Tooltip("How far can enemy see")]
    public float visibilityRange = 20f;
    [Tooltip("Enemy see further when we move quicker than threshold")]
    public float movingVisibilityMultiplier = 1.7f;
    [Tooltip("How close can enemy sense player")]
    public float nearRange = 0.5f;
    [Tooltip("Delay until enemy fully detect player")]
    public float detectTime = 0.2f;
    private float seenTime = 0;
    [Tooltip("Increse the detection speed when player move slower than threshold")]
    public float slowDetectMultiplier = 2f;
    private bool chasePlayer = false;
    
    private void Start()
    {
        navMeshAgent.SetDestination(wayPoints[0].position);
        lastPosChange = false;
        playerBody = Player.GetComponent<Rigidbody>();
        quickThreshold = Mathf.Sqrt(quickThreshold);
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
            if (hit.transform.CompareTag("Player") &&
                hit.distance < visibilityRange * (playerBody.velocity.sqrMagnitude > quickThreshold ? movingVisibilityMultiplier : 1))
            {
                if (hit.distance < nearRange)
                {
                    // make enemy able to sense if player is very close
                    seenTime += Time.deltaTime * (playerBody.velocity.sqrMagnitude < quickThreshold ? slowDetectMultiplier : 1);
                    if (seenTime > detectTime)
                    {
                        SetPos();
                        chasePlayer = true;
                    }
                }
                else if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position)) < lineOfSight)
                {
                    // if player in forward line of sight
                    seenTime += Time.deltaTime * (playerBody.velocity.sqrMagnitude < quickThreshold ? slowDetectMultiplier: 1);
                    if (seenTime > detectTime)
                    {
                        SetPos();
                        chasePlayer = true;
                    }
                }
            }
            else
            {
                seenTime = 0;
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
                int i = 0;
                int indexOfBest = 0;
                float bestMagnitude = Mathf.Infinity;
                foreach(Transform wayPoint in wayPoints)
                {
                    float thisMagnitude = (wayPoint.position - transform.position).sqrMagnitude;
                    if(thisMagnitude < bestMagnitude)
                    {
                        bestMagnitude = thisMagnitude;
                        indexOfBest = i;
                    }
                    i++;
                }
                m_currIndex = indexOfBest;
            }
            else
            {
                m_currIndex = (m_currIndex + 1) % wayPoints.Length;
            }
            navMeshAgent.SetDestination(wayPoints[m_currIndex].position);
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
