using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Map.PathFinding
{
    public class Node: IHeapItem<Node>
    {
        public bool walkable;
        public Vector3 worldPosition;
        public int gridX;
        public int gridY;

        public int gCost;
        public int hCost;

        public Node parent;
        private int _heapIndex;

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

        public int HeapIndex
        {
            get
            {
                return _heapIndex;
            }
            set
            {
                _heapIndex = value;
            }
        }

        public int CompareTo(Node nodeToCompare)
        {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if(compare == 0)
            {
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }

            return -compare;
        }
    }
}
