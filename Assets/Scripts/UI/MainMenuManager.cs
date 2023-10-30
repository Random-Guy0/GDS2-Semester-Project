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

    [SerializeField] private CanvasGroup[] canvasGroup;

    private void Start()
    {
        canvasGroup[0].alpha = 1f;
        canvasGroup[0].interactable = true;
        canvasGroup[1].alpha = 0f;
        canvasGroup[1].interactable = false;
    }
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

    public void LoadLevelSelectNew()
    {
        canvasGroup[0].alpha = 0f;
        canvasGroup[0].interactable = false;
        canvasGroup[1].alpha = 1f;
        canvasGroup[1].interactable = true;
    }

    public void ReturnToMainMenu()
    {
        canvasGroup[1].alpha = 0f;
        canvasGroup[0].interactable = true;
        canvasGroup[0].alpha = 1f;
        canvasGroup[1].interactable = false;
    }
    public void doExitGame()
    {
        Application.Quit();
    }
    
}
