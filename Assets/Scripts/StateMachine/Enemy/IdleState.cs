using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState
{
    private NavMeshAgent m_NavMeshAgent;
    private GameObject m_OwnerGameObject;
    private Animator m_Animator;

    //TODO Do we want to add animations into these states aswell.

    public IdleState(NavMeshAgent m_NavMeshAgent, GameObject m_OwnerGameObject, Animator m_Animator)
    {
        this.m_NavMeshAgent = m_NavMeshAgent;
        this.m_OwnerGameObject = m_OwnerGameObject;
        this.m_Animator = m_Animator; 
    }

    public void BeginAnimation()
    {
        m_Animator.SetBool("isIdle", true);
        m_Animator.SetBool("isAttacking", false);
        m_Animator.SetBool("isMoving", false);
    }

    public void Enter()
    {
        Debug.Log(m_OwnerGameObject.name + " has entered idle state");
        BeginAnimation();
    }

    public void Execute()
    {
        Debug.Log(m_OwnerGameObject.name + " is currently executing idle state");
        m_NavMeshAgent.SetDestination(m_OwnerGameObject.transform.position);
    }

    public void Exit()
    {
        Debug.Log(m_OwnerGameObject.name + " has exited idle state");

    }
}
