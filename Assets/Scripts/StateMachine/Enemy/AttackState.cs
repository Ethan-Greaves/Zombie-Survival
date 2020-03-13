using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private GameObject m_OwnerGameObject;
    private Animator m_Animator;

    public AttackState(GameObject m_OwnerGameObject, Animator m_Animator)
    {
        this.m_OwnerGameObject = m_OwnerGameObject;
        this.m_Animator = m_Animator;
    }

    public void BeginAnimation()
    {
        m_Animator.SetBool("isAttacking", true);
        m_Animator.SetBool("isMoving", false);
        m_Animator.SetBool("isIdle", false);
    }

    public void Enter()
    {
        //Debug.Log(m_OwnerGameObject.name + " has entered attack state");
        BeginAnimation();
    }

    public void Execute()
    {
        //Debug.Log(m_OwnerGameObject.name + " is executing attack state");
    }

    public void Exit()
    {
        //Debug.Log(m_OwnerGameObject.name + " has exited attack state");
    }

  
}
