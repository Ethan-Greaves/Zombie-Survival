using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState m_CurrentState;
    private IState m_PreviousState;

    public void ChangeState(IState newState)
    {
        if(m_CurrentState != null)
        {
            m_CurrentState.Exit();
        }

        m_PreviousState = m_CurrentState;
        m_CurrentState = newState;
        m_CurrentState.Enter();
    }

    public void UpdateState()
    {
        IState runningState = m_CurrentState;

        if (runningState != null)
            m_CurrentState.Execute();
    }

    public void GoToPreviousState()
    {
        m_CurrentState.Exit();
        m_CurrentState = m_PreviousState;
        m_CurrentState.Enter();
    }
}
