using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Grid
{
    public class Node
    {
        public bool walkable;
        public Vector3 worldPosition;
        public int gridX;
        public int gridY;

        public int gCost;
        public int hCost;

        public Node parent;

        public Node(bool walkable, Vector3 worldPosition, int x, int y)
        {
            this.walkable = walkable;
            this.worldPosition = worldPosition;
            gridX = x;
            gridY = y;
        }

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }
    }
}
