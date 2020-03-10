using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] protected AudioClip[] m_EnemyNoises;
    [SerializeField] protected AudioClip m_EnemyDamageTakenSFX;

    [Header("Noise Rate")]
    [SerializeField] protected float m_EnemyNoiseRateMin;
    [SerializeField] protected float m_EnemyNoiseRateMax;
    
    protected float m_EnemyNoiseDelay;
    private float m_EnemyNoiseRate;

    private void Awake()
    {
        m_EnemyNoiseRate = Random.Range(m_EnemyNoiseRateMin, m_EnemyNoiseRateMax);
        m_EnemyNoiseDelay = m_EnemyNoiseRate;
    }

    public void TakeDamageSFX()
    {
        SoundManager.Instance().PlaySFX(m_EnemyDamageTakenSFX);
    }

    public void PlayNoisesSFX()
    {
        m_EnemyNoiseDelay -= Time.deltaTime;

        if (m_EnemyNoiseDelay <= 0)
        {
            m_EnemyNoiseDelay = m_EnemyNoiseRate;
            SoundManager.Instance().PlayRandomSFX(m_EnemyNoises);
        }
    }
}
