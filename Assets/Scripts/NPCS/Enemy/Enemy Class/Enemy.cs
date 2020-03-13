using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(EnemySFX), typeof(EnemyAI), typeof(EnemyAnimation))]
[RequireComponent(typeof(EnemyVFX))]
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int m_Damage;
    [SerializeField] protected int m_Health;
    [Header("Pickups To Drop")]
    [SerializeField] Pickup[] m_PickupsToDrop;

    protected EnemySFX m_EnemySFX;
    protected EnemyAI m_EnemyAI;
    protected EnemyAnimation m_EnemyAnimation;
    protected EnemyVFX m_EnemyVFX;

    private StateMachine m_EnemyStateMachine;
    private IdleState m_IdleState;
    private ChaseState m_ChaseState;
    private AttackState m_AttackState;
    private Player m_Player;

    public UnityAction m_OnDeath;

    #region GETTERS
    public UnityAction GetOnDeath() { return m_OnDeath; }
    #endregion

    #region INITILISATION
    void Awake()
    {
        m_Health = 100;
        m_EnemySFX = GetComponent<EnemySFX>();
        m_EnemyAI = GetComponent<EnemyAI>();
        m_EnemyAnimation = GetComponent<EnemyAnimation>();
        m_EnemyVFX = GetComponent<EnemyVFX>();

        m_EnemyStateMachine = new StateMachine();

        m_IdleState = new IdleState(m_EnemyAI.GetNavMeshAgent(), this.gameObject, m_EnemyAnimation.GetAnimator());
        m_ChaseState = new ChaseState(this.gameObject, m_EnemyAI.GetNavMeshAgent(), m_EnemyAI.GetPlayer(), m_EnemyAnimation.GetAnimator());
        m_AttackState = new AttackState(this.gameObject, m_EnemyAnimation.GetAnimator());

        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_EnemyStateMachine.ChangeState(m_IdleState);
    }

    #endregion

    #region UPDATE LOOPS
    // Update is called once per frame
    void Update()
    {
        m_EnemySFX.PlayNoisesSFX();

        m_EnemyStateMachine.UpdateState();
        ChangeStatesBasedOffPlayerDistance();
    }

    private void LateUpdate()
    {
        //Put this in a later execution order so update has a chance to set enemy to null
        //CheckToKillEnemy();
    }

    #endregion

    #region STATE FUNCTIONALITY
    private void ChangeStatesBasedOffPlayerDistance()
    {
        if (m_Player.GetIsPlayerDead() == false)
        {
            if (m_EnemyAI.GetIsInChaseRange())
            {
                m_EnemyStateMachine.ChangeState(m_ChaseState);
            }
            if (m_EnemyAI.GetIsInAttackRange())
            {
                m_EnemyStateMachine.ChangeState(m_AttackState);
            }
            if (!m_EnemyAI.GetIsInChaseRange() && !m_EnemyAI.GetIsInAttackRange())
            {
                m_EnemyStateMachine.ChangeState(m_IdleState);
            }
        }
        else if (m_Player.GetIsPlayerDead() == true) 
            m_EnemyStateMachine.ChangeState(m_IdleState);
    }

    #endregion

    #region TAKE DAMAGE FUNCTIONALITY
    public void TakeDamage(int damage)
    {
        if(this.gameObject != null)
        {
            m_Health -= damage;
            m_EnemyVFX.TakeDamageVFX();
            m_EnemySFX.TakeDamageSFX();

            if (m_Health < 100)
                m_EnemyAI.SetChaseRange(m_EnemyAI.GetDistanceBetween());

            if (m_Health <= 0)
                KillEnemy();
        }
    }

    private void KillEnemy()
    {
        DropPowerup();
        GameManager.Instance().AddScore(5);

        if (m_OnDeath != null)
            m_OnDeath();

        Destroy(gameObject);
    }

    private void DropPowerup()
    {
        Instantiate(m_PickupsToDrop[Random.Range(0, m_PickupsToDrop.Length)], transform.position, transform.rotation);
    }


    #endregion

    #region ATTACK FUNCTIONALITY
    public void DealDamage() { m_Player.TakeDamage(m_Damage); }
    #endregion
}
