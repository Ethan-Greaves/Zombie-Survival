using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] int m_NumberOfEnemies;
    [SerializeField] float m_SpawnDelay;
    [SerializeField] int m_EnemiesToSpawnPerDelay;
    [SerializeField] Enemy[] m_EnemyTypes;
   

    #region GETTERS
    public int GetNumberOfEnemies() { return m_NumberOfEnemies; }
    public float GetSpawnDelay() { return m_SpawnDelay; }
    public int GetEnemiesToSpawnPerDelay() { return m_EnemiesToSpawnPerDelay; }
    public Enemy[] GetEnemyTypes() { return m_EnemyTypes; }
    public Enemy GetRandomEnemy() { return m_EnemyTypes[Random.Range(0, m_EnemyTypes.Length)]; }
    

    #endregion


}
