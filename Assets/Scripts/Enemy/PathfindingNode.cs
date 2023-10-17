using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathfindingNode : MonoBehaviour
{
    [SerializeField] private bool hasCollision;
    private List<GameObject> collisionObjects = new List<GameObject>();
    
    public float GScore { get; set; }
    public float HScore { get; set; }
    public PathfindingNode CameFrom { get; set; }

    private List<PathfindingNode> _connectedNodes;
    public List<PathfindingNode> ConnectedNodes
    {
        get
        {
            return _connectedNodes.Where(node => !node.hasCollision).ToList();
        }
        set => _connectedNodes = value;
    }
    public Vector2Int GridPosition { get; set; }
    
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
        return !hasCollision;
    }

    public bool IsMovable(GameObject other)
    {
        return !hasCollision || collisionObjects.Contains(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        hasCollision = true;
        collisionObjects.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        hasCollision = false;
        collisionObjects.Remove(other.gameObject);
    }
}
