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

        private PathfindingGrid _grid;

        private void Start()
        {
            _grid = GetComponent<PathfindingGrid>();
        }

        public void GenerateDungeon()
        {
            tilemapVisualizer.Clear();
            RunProceduralGeneration();

            if (_grid)
            {
                StartCoroutine(_grid.UpdateGrid());
            }

        }

        protected abstract void RunProceduralGeneration();
    }
}

