#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathfindingGrid))]
public class PathfindingGridEditor : Editor
{
    private Vector2Int gridSize;
    private float gridNodeSpacing;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PathfindingGrid pathfindingGrid = (PathfindingGrid)target;

        if (gridSize != pathfindingGrid.GridSize || gridNodeSpacing != pathfindingGrid.GridNodeSpacing)
        {
            gridSize = pathfindingGrid.GridSize;
            gridNodeSpacing = pathfindingGrid.GridNodeSpacing;
            //pathfindingGrid.CreateGrid();
        }
    }
}
#endif