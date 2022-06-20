using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    [SerializeField] private LayerMask _unWalkableMask;
    [SerializeField] private float _nodeRadius;
    [SerializeField] private Vector2 _gridWorldSize;

    private Node[,] _grid;
    private float _nodeDiameter;
    private int _gridSizeX, _gridSizeY;

    private void Start()
    {
        _nodeDiameter = _nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);
        CreateGrid();
    }

    private void CreateGrid()
    {
        _grid = new Node[_gridSizeX, _gridSizeY];

        Vector2 worldBottomLeftPosition = (Vector2)transform.position
            - Vector2.right * _gridWorldSize.x / 2
            - Vector2.up * _gridWorldSize.y / 2;

        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeftPosition
                    + Vector2.right * (x * _nodeDiameter + _nodeRadius)
                    + Vector2.up * (y * _nodeDiameter + _nodeRadius);

                bool walkable = (Physics2D.OverlapCircle(worldPoint, _nodeRadius, _unWalkableMask) == null);
                _grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for(int x=-1; x<=1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 & y == 00)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                bool isValidNode = checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY;

                if (isValidNode)
                {
                    neighbours.Add(_grid[checkX, checkY]);
                }

            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = Mathf.Clamp01((worldPosition.x + _gridWorldSize.x / 2) / _gridWorldSize.x);
        float percentY = Mathf.Clamp01((worldPosition.y + _gridWorldSize.y / 2) / _gridWorldSize.y);

        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

        return _grid[x, y];
    }


    public List<Node> path; 
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(_gridWorldSize.x, _gridWorldSize.y));
        if (_grid != null)
        {
            foreach (Node n in _grid)
            {
                Gizmos.color = Color.red;

                if (n.walkable)
                {
                    Gizmos.color = Color.white;
                }

                if(path != null && path.Contains(n))
                {
                    Gizmos.color = Color.black;
                }

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (_nodeDiameter - .1f));
            }
        }
    }
}
