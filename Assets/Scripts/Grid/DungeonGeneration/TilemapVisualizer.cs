using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TheProphecy.Grid.DungeonGeneration
{
    public class TilemapVisualizer : MonoBehaviour
    {
        [SerializeField] private Tilemap floorTilemap, wallTilemap;
        [SerializeField] private TileBase floorTile, wallTop, 
            wallSideRight, wallSiderLeft, wallBottom, wallFull,
            wallInnerCornerDownLeft, wallInnerCornerDownRight,
            wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

        public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
        {
            PaintFloorTiles(floorPositions, floorTilemap, floorTile);
        }

        private void PaintFloorTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
        {
            foreach(var position in positions)
            {
                PaintSingleTile(tilemap, tile, position);
            }
        }

        public void PaintSingleBasicWall(Vector2Int position, string binaryType)
        {
            int typeAsInt = Convert.ToInt32(binaryType, 2);
            PaintSingleTile(wallTilemap, wallTop, position);
        }

        private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
        }

        internal void PaintSingleCornerWall(Vector2Int position, string neighboursBinaryType)
        {
            throw new NotImplementedException();
        }
    }

}
