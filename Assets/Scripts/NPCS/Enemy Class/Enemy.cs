using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySFX), typeof(EnemyAI), typeof(EnemyAnimation))]
[RequireComponent(typeof(EnemyVFX))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int m_MoveSpeed;
    [SerializeField] protected int m_Damage;
    [SerializeField] protected int m_Health;

    protected EnemySFX m_EnemySFX;
    protected EnemyAI m_EnemyAI;
    protected EnemyAnimation m_EnemyAnimation;
    protected EnemyVFX m_EnemyVFX;

    protected abstract void NormalAttack();
    protected abstract void SpecialAttack();

    // Start is called before the first frame update
    void Awake()
    {
        m_Health = 100;
        m_EnemySFX = GetComponent<EnemySFX>();
        m_EnemyAI = GetComponent<EnemyAI>();
        m_EnemyAnimation = GetComponent<EnemyAnimation>();
        m_EnemyVFX = GetComponent<EnemyVFX>();
    }

    // Update is called once per frame
    void Update()
    {
        m_EnemySFX.PlayNoisesSFX();
        CheckToKillEnemy();
    }

    public void TakeDamage(int damage)
    {
        if(this.gameObject != null)
        {
            m_Health -= damage;
            m_EnemyVFX.TakeDamageVFX();
            m_EnemySFX.TakeDamageSFX();
        }
    }

    private void CheckToKillEnemy()
    {
        if(m_Health <= 0)
        {
            GameManager.Instance().AddScore(5);
            Destroy(gameObject);
        }
    }

    private void DropPowerupOnDeath()
    {

    }

}
