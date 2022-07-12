using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TheProphecy.Map.DungeonGeneration
{
    public class TilemapVisualizer : MonoBehaviour
    {
        [SerializeField] private Tilemap floorTilemap, wallTilemap;
        [SerializeField] private TileBase floorTile, wallTop, 
            wallSideRight, wallSideLeft, wallBottom, wallFull,
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
            TileBase tile = null;

            if (WallTypesHelper.wallTop.Contains(typeAsInt))
            {
                tile = wallTop;
            }

            else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
            {
                tile = wallSideRight;
            }

            else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
            {
                tile = wallSideLeft;
            }

            else if (WallTypesHelper.wallBottom.Contains(typeAsInt))
            {
                tile = wallBottom;
            }

            else if (WallTypesHelper.wallFull.Contains(typeAsInt))
            {
                tile = wallFull;
            }

            if (tile != null)
            {
                PaintSingleTile(wallTilemap, tile, position);
            }
            
        }

        public void PaintSingleCornerWall(Vector2Int position, string binaryType)
        {
            int typeAsInt = Convert.ToInt32(binaryType, 2);
            TileBase tile = null;

            if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
            {
                tile = wallInnerCornerDownLeft;
            }
            else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
            {
                tile = wallInnerCornerDownRight;
            }
            else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerDownLeft;
            }
            else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerDownRight;
            }
            else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerUpRight;
            }
            else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerUpLeft;
            }
            else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
            {
                tile = wallFull;
            }
            else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
            {
                tile = wallBottom;
            }

            if (tile != null)
            {
                PaintSingleTile(wallTilemap, tile, position);
            }

        }

        private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
        {
            Vector3Int tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
        }


    }

}
