using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneManager")]
public class SceneHandler : ScriptableObject
{
    //Create a stack to store all previous scenes
    private Stack<int> m_LoadedLevels;

    [System.NonSerialized]
    private bool m_Initialised;

    private void OnEnable()
    {
        m_LoadedLevels = new Stack<int>();
        m_Initialised = true;
    }

    public UnityEngine.SceneManagement.Scene GetActiveScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }


    public void LoadSceneByNameSingle(string sceneName)
    {
        //Put the current scene onto the stack, essentially saving it
        m_LoadedLevels.Push(GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadSceneByNameAdditive(string sceneName)
    {
        //Put the current scene onto the stack, essentially saving it
        m_LoadedLevels.Push(GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void LoadPreviousScene()
    {
        if(m_LoadedLevels.Count > 0)
            SceneManager.LoadScene(m_LoadedLevels.Pop());
        else
            Debug.Log("No previous scenes");
    }

    public void RemoveScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadMenu()
    {
        //Put the current scene onto the stack, essentially saving it
        m_LoadedLevels.Push(GetActiveScene().buildIndex);

        GameManager.Instance().ResetScore();
        GameManager.Instance().ResumeGame();
        SceneManager.LoadScene("Main Menu");
    }

    //This function will only work on standalone builds of the game.
    public void ExitGame()
    {
        Application.Quit();
    }
}


