using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    static SceneHandler m_SceneHandlerInstance;

    //Used for initialising variables or game states
    private void Awake()
    {
        m_SceneHandlerInstance = new SceneHandler();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public static SceneHandler Instance()
    {
        if(m_SceneHandlerInstance == null)
        {
            m_SceneHandlerInstance = new SceneHandler();
        }

        return m_SceneHandlerInstance;
    }

    public void LoadNextScene()
    {
        //Get our active scene, return its index.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByName(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName, mode);
    }

    public void ReturnToMenu()
    {
        GameManager.Instance().SetIsPaused(false);
        int startMenu = 0;
        SceneManager.LoadScene(startMenu);
    }

    public void RemoveScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    //This function will only work on standalone builds of the game.
    public void ExitGame()
    {
        Application.Quit();
    }
}


