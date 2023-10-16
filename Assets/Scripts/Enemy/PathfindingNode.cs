using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode : MonoBehaviour
{
    [SerializeField] private bool hasCollision;
    
    public float GScore { get; set; }
    public float HScore { get; set; }
    public PathfindingNode CameFrom { get; set; }

    public List<PathfindingNode> ConnectedNodes { get; private set; } = new List<PathfindingNode>();
    
    public float FScore
    {
        get => GScore + HScore;
    }
    
    public float GridNodeSpacing
    {
        set
        {
            BoxCollider2D col = GetComponent<BoxCollider2D>();

            col.size = Vector2.one * (value - 0.1f);
        }
    }

    public bool IsMovable()
    {
        return hasCollision;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        hasCollision = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        hasCollision = false;
    }
}
