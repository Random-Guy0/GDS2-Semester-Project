using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public void AdvanceScene()
    {
        // Button from Tutorial
        if (SceneManager.GetActiveScene().name == "1-Tutorial-to-Egypt")
        {
            SceneManager.LoadScene("Egyptian Level");
        }
        else if (SceneManager.GetActiveScene().name == "2-Egypt-to-Babylon")
        {
            SceneManager.LoadScene("Babylon Level");
        }
        else if (SceneManager.GetActiveScene().name == "Victory Scene")
        {
            SceneManager.LoadScene("MainMenuStart");
        }
    }
}
