using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private PathfindingGrid pathfinding;

    protected virtual Transform Target
    {
        get => GameManager.Instance.Player.transform;
    }

    private void Start()
    {
        pathfinding = GameManager.Instance.GetPathfindingGridAtPosition(transform.position);
    }
}
