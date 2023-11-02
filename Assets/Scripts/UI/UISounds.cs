using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter acceptSound;
    [SerializeField] private StudioEventEmitter backSound;

    public void Accept()
    {
        acceptSound.Play();
    }

    public void Back()
    {
        backSound.Play();
    }
}
