using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundFade : MonoBehaviour
{
    [SerializeField] private SpriteRenderer foreground;
    [SerializeField] private float foregroundOpacity;
    [SerializeField] private float fadeDuration;
    private Color fadedColor;
    private Color normalColor;

    private void Start()
    {
        normalColor = new Color(1f, 1f, 1f, 1f);
        if (foregroundOpacity == 0) { fadedColor = new Color(1f, 1f, 1f, 0.3f); }
        else { fadedColor = new Color(1f, 1f, 1f, foregroundOpacity); }

        if (fadeDuration == 0) { fadeDuration = 0.5f; }
    }

    IEnumerator ColourShiftFade(Color start, Color end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            foreground.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        foreground.color = end;
    }

    IEnumerator ColourShiftSolid(Color start, Color end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            foreground.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        foreground.color = end;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            StartCoroutine(ColourShiftFade(foreground.color, fadedColor, fadeDuration));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ColourShiftSolid(foreground.color, normalColor, fadeDuration));
        }
    }

}
