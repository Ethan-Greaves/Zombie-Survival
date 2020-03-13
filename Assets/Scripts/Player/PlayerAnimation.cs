using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void AddMovementAnimation(ref Vector3 vectorInput)
    {
        //TODO Animation needs to be tied to the motion of the character not the player input
        m_Animator.SetFloat("VerticalForward", vectorInput.z);
        m_Animator.SetFloat("VerticalBackward", vectorInput.z);
        m_Animator.SetFloat("HorizontalLeft", vectorInput.x);
        m_Animator.SetFloat("HorizontalRight", vectorInput.x);
    }

    public void PlayDeathAnimation() { m_Animator.SetBool("IsDead", true); }
}
