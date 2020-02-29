using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int m_MoveSpeed;
    [SerializeField] protected int m_Damage;
    [SerializeField] protected int m_Health;

    [Header("VFX")]
    [SerializeField] protected ParticleSystem m_TakeDamageVFX;

    [Header("SFX")]
    [SerializeField] protected AudioClip[] m_EnemyNoises;
    [SerializeField] protected float m_EnemyNoiseRate;
    [SerializeField] protected AudioClip m_EnemyDamageTakenSFX;

    [Header("Misc")]
    [SerializeField] protected Player m_Player;
    
    protected float m_EnemyNoiseDelay;

    private NavMeshAgent m_NavMeshAgent;
    private Rigidbody m_RigidBody;
    private Animator m_Animator;

    protected abstract void NormalAttack();
    protected abstract void SpecialAttack();

    // Start is called before the first frame update
    void Awake()
    {
        m_EnemyNoiseDelay = m_EnemyNoiseRate;
        m_Health = 100;

        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayNoisesSFX();
        CheckToKillEnemy();
        //m_NavMeshAgent.SetDestination(m_Player.transform.position);
        m_Animator.SetBool("isMoving", true);
    }

    private void CheckForMotion()
    {

    }

    public void TakeDamage(int damage)
    {
        m_Health  -= damage;
        TakeDamageVFX();
        TakeDamageSFX();
    }

    private void TakeDamageVFX()
    {
        if (m_TakeDamageVFX != null)
        {
            //Spawn the vfx
            ParticleSystem tempTakeDamageVFX = Instantiate(m_TakeDamageVFX, gameObject.transform.position, gameObject.transform.rotation);
            tempTakeDamageVFX.Play();

            //Destroy the VFX after a small period
            Destroy(tempTakeDamageVFX.gameObject, 0.2f);
        }
        else
            Debug.LogWarning("m_TakeDamageVFX Has not been assigned!!!");
    }

    private void TakeDamageSFX()
    {
        SoundManager.Instance().PlaySFX(m_EnemyDamageTakenSFX);
    }

    private void PlayNoisesSFX()
    {
        m_EnemyNoiseDelay -= Time.deltaTime;

        if (m_EnemyNoiseDelay <= 0)
        {
            m_EnemyNoiseDelay = m_EnemyNoiseRate;
            SoundManager.Instance().PlayRandomSFX(m_EnemyNoises);
        }
    }

    private void CheckToKillEnemy()
    {
        if(m_Health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
