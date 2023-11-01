using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [field: SerializeField] public GameplayUI GameplayUI { get; private set; }
    [field: SerializeField] public GameOverUI GameOverUI { get; private set; }
    [field: SerializeField] public GameObject Player { get; private set; }

    [SerializeField] private GameObject pauseUI;

    public bool Paused { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        
        UnPause();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuStart");
    }

    private void Update()
    {
        Debug.Log(Keyboard.current.escapeKey.wasPressedThisFrame);
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (Paused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        Paused = true;
        pauseUI.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        Paused = false;
        pauseUI.SetActive(false);
    }
}
