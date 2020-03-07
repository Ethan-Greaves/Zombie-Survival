using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void StartMovingAnimation()
    {
        m_Animator.SetBool("isMoving", true);
    }
    public void StartIdleAnimation()
    {
        m_Animator.SetBool("isMoving", false);
    }

    public void StartAttackAnimation()
    {
        m_Animator.SetBool("isMoving", false);
        m_Animator.SetBool("isAttacking", true);
    }
}
