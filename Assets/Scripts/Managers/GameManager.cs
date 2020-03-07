using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  |-----------------------------------------------------------------------|
//  |                   What goes in a Game Manager?                        |
//  |                                                                       |
//  |  1. Score                                                             |
//  |  2. Pause Function                                                    |
//  |-----------------------------------------------------------------------|

public class GameManager : MonoBehaviour
{
    private static GameManager m_GameMangerInstance;
    private static bool m_bIsPaused;
    private static int m_iScore = 0;

    private int m_iNumOfEnemies;

    public static GameManager Instance()
    {
        if (m_GameMangerInstance == null)
            m_GameMangerInstance = new GameObject("Game Manager", typeof(GameManager)).GetComponent<GameManager>();

        return m_GameMangerInstance;
    }

    //  -------------------------------------------------------------------------------------------------------------------
    //                                                  INITILISATION
    //  -------------------------------------------------------------------------------------------------------------------

    //Used for initialising variables or game states
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        m_bIsPaused = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO find way to initialise a music audio clip var without using inspector (through code)
        //SoundManager.Instance().PlayMusic()
    }

    //  -------------------------------------------------------------------------------------------------------------------

    //  -------------------------------------------------------------------------------------------------------------------
    //                                                  PAUSE FUNCTIONS
    //  -------------------------------------------------------------------------------------------------------------------
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
        SceneHandler.Instance().LoadSceneByName("Pause Menu", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        m_bIsPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        SceneHandler.Instance().RemoveScene("Pause Menu");
        m_bIsPaused = false;
    }

    public void SetIsPaused(bool set)
    {
        m_bIsPaused = set;
    }

    //  -------------------------------------------------------------------------------------------------------------------

    //  -------------------------------------------------------------------------------------------------------------------
    //                                              SCORE FUNCTIONS
    //  -------------------------------------------------------------------------------------------------------------------

    public int GetScore() { return m_iScore; }
    public void ResetScore() { m_iScore = 0; }
    public void AddScore(int scoreToAdd) { m_iScore += scoreToAdd; } 

    //  -------------------------------------------------------------------------------------------------------------------


}
