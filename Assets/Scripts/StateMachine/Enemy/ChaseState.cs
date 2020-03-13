using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    private GameObject m_OwnerGameObject;
    private NavMeshAgent m_NavMeshAgent;
    private GameObject m_Player;
    private Animator m_Animator;

    public ChaseState(GameObject m_OwnerGameObject, NavMeshAgent m_NavMeshAgent, GameObject m_Player, Animator m_Animator)
    {
        this.m_OwnerGameObject = m_OwnerGameObject;
        this.m_NavMeshAgent = m_NavMeshAgent;
        this.m_Player = m_Player;
        this.m_Animator = m_Animator;
    }

    public void BeginAnimation()
    {
        m_Animator.SetBool("isMoving", true);
        m_Animator.SetBool("isAttacking", false);
        m_Animator.SetBool("isIdle", false);
    }

    public void Enter()
    {
        //Debug.Log(m_OwnerGameObject.name + " has entered Chasing state");
        BeginAnimation();
    }

    public void Execute()
    {
        //Debug.Log(m_OwnerGameObject.name + " is currently executing Chasing state");

        m_OwnerGameObject.transform.LookAt(m_Player.transform.position);

        if(m_Player != null)
            m_NavMeshAgent.SetDestination(m_Player.transform.position);
    }

    public void Exit()
    {
        //Debug.Log(m_OwnerGameObject.name + " has exited Chasing state");
    }

    
}
