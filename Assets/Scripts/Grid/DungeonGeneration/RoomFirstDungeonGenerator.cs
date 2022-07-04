using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheProphecy.Grid.DungeonGeneration 
{
    public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonCreator
    {

        [SerializeField] private int minRoomWidth = 4, minRoomHeight = 4;
        [SerializeField] private int dungeonWidth = 20, dungeonHeigth = 20;
        [SerializeField] [Range(0, 10)] private int offset = 1;
        [SerializeField] private bool randomWalkRooms = false;
        private Vector3 playerRoomPosition;

        protected override void RunProceduralGeneration()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {
            List<Room> roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
                new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeigth, 0)),
                minRoomWidth,
                minRoomHeight
            );

            HashSet<Vector2Int> floor = CreateSimpleRooms(roomsList);
            List<Vector2Int> roomCenters = new List<Vector2Int>();

            if (randomWalkRooms)
            {
                floor = CreateRoomsRandomly(roomsList);
            }

            for (int i = 0; i < roomsList.Count; i++)
            {
                Room room = roomsList[i];
                roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.Bounds.center));

                if (i == 0)
                {
                    room.roomType = RoomType.PLAYER_SPAWN;
                    playerRoomPosition = room.Bounds.center;
                }

            }

            HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);

            floor.UnionWith(corridors);
            tilemapVisualizer.PaintFloorTiles(floor);
            WallGenerator.CreateWalls(floor, tilemapVisualizer);
        }

        private HashSet<Vector2Int> CreateRoomsRandomly(List<Room> roomsList)
        {
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
            for (int i = 0; i < roomsList.Count; i++)
            {
                Room room = roomsList[i];
                BoundsInt roomBounds = room.Bounds;
                Vector2Int roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
                HashSet<Vector2Int> roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);

                foreach (Vector2Int position in roomFloor)
                {
                    if (position.x >= (roomBounds.xMin + offset) &&
                        position.x <= (roomBounds.xMax - offset) &&
                        position.y >= (roomBounds.yMin - offset) && 
                        position.y <= (roomBounds.yMax - offset))
                    {
                        floor.Add(position);
                    }
                }
            }
            return floor;
        }

        private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
        {
            HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
            Vector2Int currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
            roomCenters.Remove(currentRoomCenter);

            while (roomCenters.Count>0)
            {
                Vector2Int closest = FindClosesPointTo(currentRoomCenter, roomCenters);
                roomCenters.Remove(closest);
                HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
                currentRoomCenter = closest;
                corridors.UnionWith(newCorridor);
            }

            return corridors;
        }

        private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
        {
            HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
            var position = currentRoomCenter;
            corridors.Add(position);

            while (position.y != destination.y)
            {
                if (destination.y > position.y)
                {
                    position += Vector2Int.up;
                }

                else if(destination.y < position.y)
                {
                    position += Vector2Int.down; 
                }

                corridors.Add(position);
            }

            while (position.x != destination.x)
            {
                if (destination.x > position.x)
                {
                    position += Vector2Int.right;
                }

                else if (destination.x < position.x)
                {
                    position += Vector2Int.left;
                }

                corridors.Add(position);
            }

            return corridors;
        }

        private Vector2Int FindClosesPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
        {
            Vector2Int closest = Vector2Int.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2Int position in roomCenters)
            {
                float currentDistance = Vector2.Distance(position, currentRoomCenter);

                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closest = position;
                }
            }

            return closest;

        }

        private HashSet<Vector2Int> CreateSimpleRooms(List<Room> roomsList)
        {
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

            foreach (Room room in roomsList)
            {
                for (int col = offset; col < room.Bounds.size.x - offset; col++)
                {
                    for (int row = offset; row < room.Bounds.size.y - offset; row++)
                    {
                        Vector2Int position = (Vector2Int)room.Bounds.min + new Vector2Int(col, row);
                        floor.Add(position);
                    }
                }
            }

            return floor;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            if (playerRoomPosition != null)
            {
                Gizmos.DrawSphere(playerRoomPosition, 0.5f);
            }
            
        }
    }


}

