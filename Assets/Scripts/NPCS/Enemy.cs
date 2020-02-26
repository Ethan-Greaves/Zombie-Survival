using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int m_Health;
    [SerializeField] protected int m_MoveSpeed;
    [SerializeField] protected int m_Damage;

    [Header("VFX")]
    [SerializeField] protected ParticleSystem m_TakeDamageVFX;

    [Header("SFX")]
    [SerializeField] protected AudioClip[] m_EnemyNoises;
    [SerializeField] protected float m_EnemyNoiseRate;
    [SerializeField] protected AudioClip[] m_EnemyDamageTakenSFX;
    
    protected float m_EnemyNoiseDelay;
    protected AudioSource m_AudioSource;

    protected abstract void NormalAttack();
    protected abstract void SpecialAttack();

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_EnemyNoiseDelay = m_EnemyNoiseRate;
    }

    // Update is called once per frame
    void Update()
    {
        PlayNoisesSFX();
        KillEnemy();
        
    }

    public void TakeDamage()
    {
        m_Health = 0;
        TakeDamageVFX();
        //TakeDamageSFX();

        
    }

    private void TakeDamageVFX()
    {
        //TODO Find out why the vfx is not instantiating at the enemy's position. 

        //Spawn the vfx
        ParticleSystem tempTakeDamageVFX = Instantiate(m_TakeDamageVFX, transform.position, transform.rotation);
        tempTakeDamageVFX.Play();

        //Destroy the VFX after a small period
        Destroy(tempTakeDamageVFX.gameObject, 0.2f);
    }

    private void TakeDamageSFX()
    {
        m_AudioSource.Stop();
        m_AudioSource.PlayOneShot(m_EnemyDamageTakenSFX[Random.Range(0, m_EnemyDamageTakenSFX.Length - 1)]);
    }

    private void PlayNoisesSFX()
    {
        m_EnemyNoiseDelay -= Time.deltaTime;

        if (m_EnemyNoiseDelay <= 0)
        {
            m_EnemyNoiseDelay = m_EnemyNoiseRate;
            m_AudioSource.PlayOneShot(m_EnemyNoises[Random.Range(0, m_EnemyNoises.Length - 1)]);
        }
    }

    private void KillEnemy()
    {
        if(m_Health <= 0)
        {
            Destroy(gameObject);
        }

      
    }

}
