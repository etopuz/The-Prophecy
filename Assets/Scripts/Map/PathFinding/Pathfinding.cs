using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Map.PathFinding
{
    public class Pathfinding : MonoBehaviour
    {
        private PathfindingGrid _grid;
        public PathfindingGrid Grid { get => _grid; }

        private void Awake()
        {
            _grid = GetComponent<PathfindingGrid>();
        }

        public Vector3[] FindPath(Vector3 startPos, Vector3 targetPos)
        {
            Vector3[] waypoints = new Vector3[0];
            bool pathSucces = false;

            Node startNode = _grid.NodeFromWorldPoint(startPos);
            Node targetNode = _grid.NodeFromWorldPoint(targetPos);

            startNode.parent = startNode;

            if (targetNode.walkable)
            {
                Heap<Node> openSet = new Heap<Node>(_grid.MaxSize);
                HashSet<Node> closedSet = new HashSet<Node>();
                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    Node currentNode = openSet.RemoveFirst();
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        pathSucces = true;
                        break;
                    }

                    List<Node> neigbours = _grid.GetNeighbours(currentNode);

                    for (int i = 0; i < neigbours.Count; i++)
                    {
                        Node neighbour = neigbours[i];
                        if (!neighbour.walkable || closedSet.Contains(neighbour))
                        {
                            continue;
                        }

                        int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, targetNode);
                            neighbour.parent = currentNode;

                            if (!openSet.Contains(neighbour))
                            {
                                openSet.Add(neighbour);
                            }
                            else
                            {
                                openSet.UpdateItem(neighbour);
                            }
                        }
                    }
                }
            }

            if (pathSucces)
            {
                return RetracePath(startNode, targetNode);
            }
            return waypoints;
        }



        private Vector3[] RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;
            path.Add(currentNode);

            while (currentNode != startNode)
            {
                path.Add(currentNode.parent);
                currentNode = currentNode.parent;
            }

            _grid.path = path;
            Vector3[] waypoints = SimplifyPath(path);
            Array.Reverse(waypoints);
            return waypoints;
        }

        private Vector3[] SimplifyPath(List<Node> path)
        {
            List<Vector3> waypointList = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;

            for (int i = 1; i < path.Count; i++)
            {
                Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
                if (directionNew != directionOld)
                {
                    waypointList.Add(path[i-1].worldPosition);
                }
                directionOld = directionNew;
            }
            return waypointList.ToArray();

        }

        private int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            return (14 * Mathf.Min(dstX, dstY) + 10 * Mathf.Abs(dstX - dstY));
        }
    }
}
