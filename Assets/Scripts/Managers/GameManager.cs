using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_GameMangerInstance;
    private SceneHandler m_SceneHandler;
    private Player m_Player;
    private static bool m_bIsPaused;
    private static int m_iScore = 0;
    private int m_iNumOfEnemies;

    public static GameManager Instance()
    {
        if (m_GameMangerInstance == null)
            m_GameMangerInstance = new GameObject("Game Manager", typeof(GameManager)).GetComponent<GameManager>();

        return m_GameMangerInstance;
    }

    #region INITILISATION

    //Used for initialising variables or game states
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        m_bIsPaused = false;
        m_SceneHandler = Resources.Load<SceneHandler>("SceneManager");

        if(m_Player != null)
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();

        //if (m_MainLevelMusic != null)
            
    }
    #endregion

    #region PAUSE FUNCTIONALITY
    public void PauseGame()
    {
        if (m_bIsPaused)
            ResumeGame();
        else
            RunPauseFunctionality();
    }

    private void RunPauseFunctionality()
    {
        Time.timeScale = 0f;
        m_SceneHandler.LoadSceneByNameAdditive("Pause Menu");
        m_bIsPaused = true;
        SoundManager.Instance().PauseMusic();
    }

    public void ResumeGame()
    {
        if(m_SceneHandler.IsCurrentScene("Main Level"))
            m_SceneHandler.RemoveScene("Pause Menu");

        Time.timeScale = 1f;
        m_bIsPaused = false;

        SoundManager.Instance().ResumeMusic();
    }

    public void SetIsPaused(bool set)
    {
        m_bIsPaused = set;
    }

    #endregion

    #region SCORE FUNCTIONALITY

    public int GetScore() { return m_iScore; }
    public void ResetScore() { m_iScore = 0; }
    public void AddScore(int scoreToAdd) { m_iScore += scoreToAdd; }
    #endregion

    #region GAME STATE FUNCTIONALITY
    public void GameOver()
    {
        m_SceneHandler.LoadSceneByNameSingle("Game Over");
        SoundManager.Instance().StopMusic();
    }
    public void LevelComplete()
    {
        m_SceneHandler.LoadSceneByNameSingle("Level Complete");
        SoundManager.Instance().StopMusic();
    }
    #endregion

}
