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
    [field: SerializeField] public GameObject Player { get; private set; }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
