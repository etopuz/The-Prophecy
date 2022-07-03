using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Grid.DungeonGeneration
{
    public abstract class AbstractDungeonGenerator : MonoBehaviour
    {
        [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;
        [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

        private CustomGrid _grid;

        private void Start()
        {
            _grid = GetComponent<CustomGrid>();
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

