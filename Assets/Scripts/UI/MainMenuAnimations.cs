using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimations : MonoBehaviour
{
    [SerializeField] private Image image;

    [SerializeField] private Sprite[] spriteArray;
    [SerializeField] private float speed = 0.2f;

    private int spriteIndex;
    private bool isDone;
    Coroutine animCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        StartUIAnim();
    }

    public void StartUIAnim()
    {
        isDone = false;
        StartCoroutine(PlayAnimUI());
    }

    public void StopUIAnim()
    {
        isDone = true;
        StopCoroutine(PlayAnimUI());
    }

    private IEnumerator  PlayAnimUI()
    {
        yield return new WaitForSeconds(speed);

        if (spriteIndex >= spriteArray.Length)
        {
            spriteIndex = 0;
        }
        image.sprite = spriteArray[spriteIndex];
        spriteIndex++;

        if (!isDone)
            animCoroutine = StartCoroutine(PlayAnimUI());
    }
}
