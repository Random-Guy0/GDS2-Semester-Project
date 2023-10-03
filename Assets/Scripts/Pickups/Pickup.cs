using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup<T> : MonoBehaviour
{
    protected abstract void Collect(T collector);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<T>(out T collector))
        {
            Collect(collector);
            Destroy(gameObject);
        }
    }
}
