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
    [SerializeField] protected AudioClip[] m_EnemyDamageTakenSFX;

    [Header("Misc")]
    [SerializeField] protected Player m_Player;
    
    protected float m_EnemyNoiseDelay;
    protected AudioSource m_AudioSource;

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

        m_AudioSource = GetComponent<AudioSource>();
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
        //Spawn the vfx
        ParticleSystem tempTakeDamageVFX = Instantiate(m_TakeDamageVFX, gameObject.transform.position, gameObject.transform.rotation);
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

    private void CheckToKillEnemy()
    {
        if(m_Health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
