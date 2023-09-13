using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBox : MonoBehaviour
{
    private float _duration;
    public float Duration
    {
        private get => _duration;
        set
        {
            _duration = value;
            StartCoroutine(WaitToDestroy());
        }
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(Duration);
        Destroy(gameObject);
    }
}
