using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] protected Player m_Player;

    private NavMeshAgent m_NavMeshAgent;
    
    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        m_NavMeshAgent.SetDestination(m_Player.transform.position);
    }
}
