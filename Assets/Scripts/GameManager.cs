using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [field: SerializeField] public GameplayUI GameplayUI { get; private set; }
    [field: SerializeField] public GameObject Player { get; private set; }

    private PathfindingGrid[] pathfindingGrids;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        pathfindingGrids = FindObjectsOfType<PathfindingGrid>();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public PathfindingGrid GetPathfindingGridAtPosition(Vector2 position)
    {
        PathfindingGrid result = null;
        for (int i = 0; i < pathfindingGrids.Length; i++)
        {
            if (pathfindingGrids[i].PositionWithinGrid(position))
            {
                result = pathfindingGrids[i];
                break;
            }
        }

        return result;
    }
}
