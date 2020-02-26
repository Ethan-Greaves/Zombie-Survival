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
    private bool isPaused;
    public static GameManager Instance()
    {
        if (m_GameMangerInstance == null)
            m_GameMangerInstance = new GameManager();

        return m_GameMangerInstance;
    }

    //Used for initialising variables or game states
    private void Awake()
    {
        m_GameMangerInstance = new GameManager();
        isPaused = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PauseGame(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0f;
            Debug.Log("Game Paused!");
            SceneHandler.Instance().LoadSceneByName("Pause Menu", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
    }

    

    public bool GetIsPaused()
    {
        //TODO remains false at all times when calling the instance of the class in other scripts because that function is static (I think) Maybe use bool isPaused argument in paused function 
        return isPaused;
    }
}
