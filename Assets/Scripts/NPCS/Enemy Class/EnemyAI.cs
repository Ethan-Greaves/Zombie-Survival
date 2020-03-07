using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    IDLE,
    ROAMING,
    CHASING,
    ATTACKING,
};

public class EnemyAI : MonoBehaviour
{
    [SerializeField] protected Player m_Player;

    private NavMeshAgent m_NavMeshAgent;
    private State m_CurrentState;
    
    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStates();
        ChangeStates();
    }

    public State GetCurrentState() { return m_CurrentState; }

    private void ChangeStates()
    {
        switch (m_CurrentState)
        {
            case State.IDLE:
            {
                    Debug.Log(gameObject.name + " Currently idle");
                    m_NavMeshAgent.SetDestination(transform.position);
                    break;
            }

            case State.CHASING:
            {
                    Debug.Log(gameObject.name + " Currently in chasing state");
                    m_NavMeshAgent.SetDestination(m_Player.transform.position);
                    break;
            }

            case State.ATTACKING:
            {
                    Debug.Log(gameObject.name + " Currently in ATTACKING state");
                    break;
            }
        }
    }

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

    private void CheckStates()
    {
        if (CheckIsDistanceBetween(1.5f, 7.5f))
            m_CurrentState = State.CHASING;
        else if (CheckIsDistanceBetween(0f, 1.5f))
            m_CurrentState = State.ATTACKING;
        else
            m_CurrentState = State.IDLE;
    }
}
