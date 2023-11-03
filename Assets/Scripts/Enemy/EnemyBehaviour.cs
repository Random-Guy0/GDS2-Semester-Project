using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private PathfindingGrid pathfinding;

    private Stack<PathfindingNode> path;

    private EnemyState state;

    private PathfindingNode currentNode;
    
    public Vector2Int GridPosition { get; set; }
    
    public bool CanMove { get; set; }

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float attackDistance = 1f;

    protected virtual Transform Target
    {
        get => GameManager.Instance.Player.transform;
    }

    private void Start()
    {
        path = new Stack<PathfindingNode>();
        //pathfinding = GameManager.Instance.GetPathfindingGridAtPosition(transform.position);
        GridPosition = pathfinding.LocalToGridPosition(pathfinding.transform.InverseTransformPoint(transform.position));
        ChangeState(EnemyState.MoveToTarget);
    }

    private void Update()
    {
        if (CanMove)
        {
            if (path.Count == 0 || !path.Peek().IsMovable(gameObject))
            {
                GeneratePathToTarget();
                Debug.Log("Path Empty");
                return;
            }

            if (currentNode == null)
            {
                currentNode = path.Pop();
            }

            Vector2 position = transform.position;
            Vector2 moveDir = (Vector2)currentNode.transform.position - position;
            position += speed * Time.deltaTime * moveDir.normalized;
            transform.position = position;

            if (Vector3.Distance(currentNode.transform.position, transform.position) < 0.01f)
            {
                transform.position = currentNode.transform.position;
                GridPosition = currentNode.GridPosition;
                currentNode = null;
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        state = newState;
        switch (state)
        {
            case EnemyState.MoveToTarget:
                GeneratePathToTarget();
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Stunned:
                break;
        }
    }

    private void GeneratePathToTarget()
    {
        CanMove = true;
        if (pathfinding.PositionWithinGrid(Target.position))
        {
            Vector2Int targetPosition =
                pathfinding.LocalToGridPosition(pathfinding.transform.InverseTransformPoint(Target.position));
            path = pathfinding.GeneratePath(GridPosition, targetPosition);
        }
    }
}
