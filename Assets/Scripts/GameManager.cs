using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [field: SerializeField] public GameplayUI GameplayUI { get; private set; }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
}
