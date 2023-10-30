using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button egyptButton;

    public void LoadLevelSelect()
    {
        //DontDestroyOnLoad(this);
        SceneManager.LoadScene(1);
    }
    
    public void LoadTutorialLevel()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadEgyptLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadBabylonLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void doExitGame()
    {
        Application.Quit();
    }
    
}
