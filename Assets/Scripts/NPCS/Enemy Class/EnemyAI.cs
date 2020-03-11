using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float m_ChaseRange = 10f;
    [SerializeField] float m_AttackRange = 2f;

    private GameObject m_Player;
    private NavMeshAgent m_NavMeshAgent;
    private float m_DistanceBetween;

    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        m_DistanceBetween = Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                             new Vector2(m_Player.transform.position.x, m_Player.transform.position.z));
    }

    #region GETTERS
    public float GetDistanceBetween() { return m_DistanceBetween; }

    public NavMeshAgent GetNavMeshAgent() { return m_NavMeshAgent; }

    public GameObject GetPlayer() { return m_Player; }

    #endregion

    #region SETTERS
    public void SetChaseRange(float newChaseRange) { m_ChaseRange = newChaseRange; }
    #endregion

    private bool CheckIsDistanceBetween(float minDistance, float maxDistance)
    {
        //If the distance between this game object and player is less than max distance AND greater than min distnace
        if ((Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
            new Vector2(m_Player.transform.position.x, m_Player.transform.position.z)) < maxDistance
            &&
            (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
            new Vector2(m_Player.transform.position.x, m_Player.transform.position.z)) > minDistance)))
        {
            return true;
        }
        else
            return false;
    }

    public bool GetIsInChaseRange()
    {
        return m_DistanceBetween < m_ChaseRange;
    }

    public bool GetIsInAttackRange()
    {
        return m_DistanceBetween < m_AttackRange;
    }

}
