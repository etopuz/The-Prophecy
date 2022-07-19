using System.Collections;
using System.Collections.Generic;
using TheProphecy.Map.PathFinding;
using UnityEngine;

namespace TheProphecy.Map.DungeonGeneration
{
    public abstract class AbstractDungeonGenerator : MonoBehaviour
    {
        [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;
        [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

        public PathfindingGrid grid;

        public void GenerateDungeon()
        {
            tilemapVisualizer.Clear();
            RunProceduralGeneration();

            if (grid)
            {
                StartCoroutine(grid.UpdateGrid());
            }

        }

        protected abstract void RunProceduralGeneration();
    }
}

