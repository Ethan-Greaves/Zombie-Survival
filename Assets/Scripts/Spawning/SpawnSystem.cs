using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] Wave[] m_Waves;

    private int m_EnemiesRemaining;
    private int m_CurrentWaveNum;
    private float m_NextSpawnTime;
    private Wave m_CurrentWave;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_EnemiesRemaining > 0 && Time.time > m_NextSpawnTime)
        {
            m_EnemiesRemaining--;
            m_NextSpawnTime = Time.time + m_CurrentWave.GetSpawnDelay();

            Instantiate(m_CurrentWave.GetRandomEnemy(), transform.position, m_CurrentWave.GetRandomEnemy().transform.rotation);

        }
    }

    private void NextWave()
    {
        m_CurrentWaveNum++;
        m_CurrentWave = m_Waves[m_CurrentWaveNum - 1];

        m_EnemiesRemaining = m_CurrentWave.GetNumberOfEnemies();


    }

   
}
