using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public void Resume()
    {
        GameManager.Instance.UnPause();
    }

    public void Retry()
    {
        GameManager.Instance.ResetGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuStart");
    }
}
