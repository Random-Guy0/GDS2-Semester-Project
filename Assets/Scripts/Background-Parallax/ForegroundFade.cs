using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundFade : MonoBehaviour
{
    [SerializeField] SpriteRenderer foreground;


    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Player has entered the trigger");

        if (other.gameObject.tag == "Player")
        {
            foreground.color = new Color(1f, 1f, 1f, 0.3f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.LogError("Player has exited the trigger");

        if (other.gameObject.tag == "Player")
        {
            foreground.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
