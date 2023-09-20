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
        //code here will load tutorial level when finished
    }

    public void LoadEgyptLevel()
    {
        SceneManager.LoadScene(2);
    }
    
}
