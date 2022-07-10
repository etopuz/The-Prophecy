using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Grid.DungeonGeneration
{
    public static class RoomArrangement
    {

        public static void SetRoomTypes(Room[,] rooms, Vector2Int dungeonEntrancePosition, List<Vector2Int> filledRoomIndexes)
        {

            Room playerSpawnRoom = rooms[dungeonEntrancePosition.x, dungeonEntrancePosition.y];
            playerSpawnRoom.roomType = RoomType.PLAYER_SPAWN;

            Room bossRoom = FindFurthestRoom(rooms, dungeonEntrancePosition, filledRoomIndexes);
            bossRoom.roomType = RoomType.BOSS_ROOM;
        }

        public static int FindDistanceBetweenRooms(Room[,] rooms, Vector2Int roomPosition, Room room, List<Vector2Int> filledRoomIndexes)
        {
            Vector2Int pos = roomPosition;

            int distance = 0;

            for (int i = 0; i < filledRoomIndexes.Count; i++)
            {
                pos = filledRoomIndexes[i];
                rooms[pos.x, pos.y].isVisited = true;

                foreach (Vector2Int direction in Direction2D.cardinalDirectionsList)
                {
                    Vector2Int neighbourPosition = pos + direction;

                    bool isNotAddable =
                       neighbourPosition.x >= rooms.GetLength(0) || neighbourPosition.y >= rooms.GetLength(1) ||
                       neighbourPosition.x < 0 || neighbourPosition.y < 0 ||
                       rooms[neighbourPosition.x, neighbourPosition.y] == null ||
                       rooms[neighbourPosition.x, neighbourPosition.y].isVisited ||
                       !rooms[pos.x, pos.y].GetNeighbourRooms().Contains(rooms[neighbourPosition.x, neighbourPosition.y]);


                    if (isNotAddable)
                    {
                        continue;
                    }

                    rooms[neighbourPosition.x, neighbourPosition.y].length = rooms[pos.x, pos.y].length + 1;

                }

                if (rooms[pos.x, pos.y] == room)
                {
                    distance = room.length;
                }
            }
            ClearRoomTravelStatus(rooms, filledRoomIndexes);
            return distance;
        }

        public static Room FindFurthestRoom(Room[,] rooms, Vector2Int roomPosition, List<Vector2Int> filledRoomIndexes)
        {
            Vector2Int pos = roomPosition;
            Room farestRoom = rooms[pos.x, pos.y];

            int maxDistance = 0;

            for (int i = 0; i < filledRoomIndexes.Count; i++)
            {
                pos = filledRoomIndexes[i];
                rooms[pos.x, pos.y].isVisited = true;
                foreach (Vector2Int direction in Direction2D.cardinalDirectionsList)
                {
                    Vector2Int neighbourPosition = pos + direction;

                    bool isNotAddable = 
                       neighbourPosition.x >= rooms.GetLength(0) || neighbourPosition.y >= rooms.GetLength(1) ||
                       neighbourPosition.x < 0 || neighbourPosition.y < 0 ||
                       rooms[neighbourPosition.x, neighbourPosition.y] == null ||
                       rooms[neighbourPosition.x, neighbourPosition.y].isVisited ||
                       !rooms[pos.x, pos.y].GetNeighbourRooms().Contains(rooms[neighbourPosition.x, neighbourPosition.y]);


                    if (isNotAddable)
                    {
                        continue;
                    }

                    rooms[neighbourPosition.x, neighbourPosition.y].length = rooms[pos.x, pos.y].length + 1;
                    
                }

                if (maxDistance < rooms[pos.x, pos.y].length)
                {
                    farestRoom = rooms[pos.x, pos.y];
                    maxDistance = rooms[pos.x, pos.y].length;
                }
            }
           
            ClearRoomTravelStatus(rooms, filledRoomIndexes);
            return farestRoom;
        }

        private static void ClearRoomTravelStatus(Room[,] rooms, List<Vector2Int> filledRoomIndexes)
        {
            for (int i = 0; i < filledRoomIndexes.Count; i++)
            {
                rooms[filledRoomIndexes[i].x, filledRoomIndexes[i].y].ClearTravelStatus();
            }
        }

        private static void DebugLengthOfRooms(Room[,] rooms)
        {
            string log = "";
            for (int i = rooms.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    if (rooms[j, i] == null)
                    {
                        log += " * ";
                    }

                    else
                    {
                        log += " " + rooms[j, i].length + " ";
                    }
                }

                log += "\n";
            }

            Debug.Log(log);
        }
    }
}

