using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathfindingGrid : MonoBehaviour
{
    [field: SerializeField] public Vector2Int GridSize { get; private set; } = Vector2Int.one;
    [field: SerializeField] public float GridNodeSpacing { get; private set; } = 0.5f;
    [SerializeField] private PathfindingNode pathfindingNodePrefab;

    private PathfindingNode[,] nodes;

    public Vector2Int LocalToGridPosition(Vector2 localPosition)
    {
        return Vector2Int.RoundToInt((localPosition - (Vector2)transform.position) / GridNodeSpacing);
    }

    public Vector2 GridToLocalPosition(Vector2Int gridPosition)
    {
        return (Vector2)gridPosition * GridNodeSpacing + (Vector2)transform.position;
    }

    private Stack<PathfindingNode> GeneratePath(PathfindingNode start, PathfindingNode end)
    {
        List<PathfindingNode> openSet = new List<PathfindingNode> { start };

        foreach (PathfindingNode node in nodes)
        {
            node.GScore = float.PositiveInfinity;
        }

        start.GScore = 0;
        start.HScore = Distance(start, end);

        while (openSet.Count > 0)
        {
            PathfindingNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FScore < currentNode.FScore)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);

            if (currentNode == end)
            {
                return ReconstructPath(start, end);
            }

            foreach (PathfindingNode connectedNode in currentNode.ConnectedNodes)
            {
                float tentativeGScore = currentNode.GScore + Distance(currentNode, connectedNode);

                if (tentativeGScore < connectedNode.GScore)
                {
                    connectedNode.CameFrom = currentNode;
                    connectedNode.GScore = tentativeGScore;
                    connectedNode.HScore = Distance(connectedNode, end);
                    if (!openSet.Contains(connectedNode))
                    {
                        openSet.Add(connectedNode);
                    }
                }
            }
        }

        return new Stack<PathfindingNode>();
    }

    private Stack<PathfindingNode> ReconstructPath(PathfindingNode start, PathfindingNode end)
    {
        Stack<PathfindingNode> path = new Stack<PathfindingNode>();
        path.Push(end);
        PathfindingNode currentNode = end;
        
        while (currentNode != start)
        {
            currentNode = currentNode.CameFrom;
            path.Push(currentNode);
        }

        return path;
    }

    private float Distance(PathfindingNode a, PathfindingNode b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }

    public void CreateGrid()
    {
        if (GridSize.x < 0)
        {
            GridSize = GridSize.y * Vector2Int.up;
        }

        if (GridSize.y < 0)
        {
            GridSize = GridSize.x * Vector2Int.right;
        }

        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        
        nodes = new PathfindingNode[GridSize.x + 1, GridSize.y + 1];

        for (int i = 0; i <= GridSize.x; i++)
        {
            for (int j = 0; j <= GridSize.y; j++)
            {
                Vector2 position = new Vector2(i, j) * GridNodeSpacing + (Vector2)transform.position;
                nodes[i, j] = Instantiate(pathfindingNodePrefab, position, Quaternion.identity, transform);
                nodes[i, j].GridNodeSpacing = GridNodeSpacing;
            }
        }
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 position = transform.position;
        Vector2[] points =
        {
            position, position + GridSize.x * GridNodeSpacing * Vector2.right, position + (Vector2)GridSize * GridNodeSpacing, position + GridSize.y * GridNodeSpacing * Vector2.up
        };
        Gizmos.color = Color.red;
        Debug.DrawLine(points[0], points[1], Color.red);
        Debug.DrawLine(points[1], points[2], Color.red);
        Debug.DrawLine(points[2], points[3], Color.red);
        Debug.DrawLine(points[3], points[0], Color.red);

        Gizmos.color = Color.blue;
        for (float i = points[0].x; i <= points[2].x; i += GridNodeSpacing)
        {
            for (float j = points[0].y; j <= points[2].y; j += GridNodeSpacing)
            {
                Gizmos.DrawWireSphere(new Vector3(i, j), 0.1f);
            }
        }
    }
    #endif
}
