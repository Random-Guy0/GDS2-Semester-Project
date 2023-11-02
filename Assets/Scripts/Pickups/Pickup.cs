using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public abstract class Pickup<T> : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter collectSound;
    protected abstract void Collect(T collector);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<T>(out T collector))
        {
            if (collectSound != null)
            {
                collectSound.Play();
            }
            
            Collect(collector);
            Destroy(gameObject);
        }
    }
}
