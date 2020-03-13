using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] Wave[] m_Waves;

    private int m_EnemiesRemainingToSpawn;
    private int m_EnemiesRemainingAlive;
    private int m_CurrentWaveNum;
    private float m_NextSpawnTime;
    private Wave m_CurrentWave;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_EnemiesRemainingToSpawn > 0 && Time.time > m_NextSpawnTime)
        {
            m_EnemiesRemainingToSpawn--;
            m_NextSpawnTime = Time.time + m_CurrentWave.GetSpawnDelay();

            Enemy spawnedEnemy = Instantiate(m_CurrentWave.GetRandomEnemy(), transform.position, m_CurrentWave.GetRandomEnemy().transform.rotation);
            spawnedEnemy.m_OnDeath += OnEnemyDeath;
        }
    }

    private void OnEnemyDeath()
    {
        m_EnemiesRemainingAlive--;

        //If there are no enemies left in the current wave then begin the next wave
        if (m_EnemiesRemainingAlive <= 0)
            StartCoroutine(NextWave());
    }

    private IEnumerator NextWave()
    {
        m_CurrentWaveNum++;
        if (m_CurrentWaveNum <= m_Waves.Length)
        {
            m_CurrentWave = m_Waves[m_CurrentWaveNum - 1];

            m_EnemiesRemainingToSpawn = m_CurrentWave.GetNumberOfEnemies();
            m_EnemiesRemainingAlive = m_EnemiesRemainingToSpawn;
        }
        else
        {
            yield return new WaitForSeconds(3);
            GameManager.Instance().LevelComplete();
        }
    }

   
}
