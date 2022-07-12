using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheProphecy.Map.DungeonGeneration
{
    public class SimpleRandomWalkDungeonCreator : AbstractDungeonGenerator
    {
        [SerializeField] protected SimpleRandomWalkSO randomWalkParameters;

        protected override void RunProceduralGeneration()
        {
            HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
            tilemapVisualizer.Clear();
            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
        {
            Vector2Int currentPosition = position;
            HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();

            for(int i = 0; i < parameters.iterations; i++)
            {
                HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLength);
                floorPosition.UnionWith(path);

                if (parameters.startRandomlyEachIteration)
                {
                    currentPosition = floorPosition.ElementAt(Random.Range(0,floorPosition.Count));
                }
            }

            return floorPosition;
        }

    }


}

