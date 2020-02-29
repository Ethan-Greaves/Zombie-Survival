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
    private static bool isPaused;

    public static GameManager Instance()
    {
        if (m_GameMangerInstance == null)
        {
            m_GameMangerInstance = new GameObject("Game Manager", typeof(GameManager)).GetComponent<GameManager>();
        }

        return m_GameMangerInstance;
    }

    //Used for initialising variables or game states
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        isPaused = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO find way to initialise a music audio clip var without using inspector (through code)
        //SoundManager.Instance().PlayMusic()
    }

    public void PauseGame()
    {
        if (isPaused)
            ResumeGame();
        else
            RunPauseFunctionality();
    }

    private void RunPauseFunctionality()
    {
        Time.timeScale = 0f;
        SceneHandler.Instance().LoadSceneByName("Pause Menu", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        SceneHandler.Instance().RemoveScene("Pause Menu");
        isPaused = false;
    }

    public void SetIsPaused(bool set)
    {
        isPaused = set;
    }
}
