using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private float cutsceneEndTime = 34.25f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndCutScene());
    }

    private IEnumerator EndCutScene()
    {
        yield return new WaitForSeconds(cutsceneEndTime);
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
