using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Map.DungeonGeneration
{
    public static class RoomArrangement
    {

        public static void SetRoomTypes(Room[,] rooms, Vector2Int dungeonEntrancePosition, List<Vector2Int> filledRoomIndexes, float specialRoomRatio)
        {

            Room playerSpawnRoom = rooms[dungeonEntrancePosition.x, dungeonEntrancePosition.y];
            playerSpawnRoom.roomType = RoomType.PLAYER_SPAWN;

            Room bossRoom = FindFurthestRoom(rooms, dungeonEntrancePosition, filledRoomIndexes);
            bossRoom.roomType = RoomType.BOSS_ROOM;

            CreateSpecialRooms(rooms, dungeonEntrancePosition, filledRoomIndexes, specialRoomRatio);

            // create different mob rooms

        }


        private static void CreateSpecialRooms(Room[,] rooms, Vector2Int dungeonStartPosition, List<Vector2Int> filledRoomIndexes, float specialRoomRatio)
        {

            int numberOfSpecialRooms = Mathf.CeilToInt(filledRoomIndexes.Count * specialRoomRatio);
            List<Room> possibleSpecialRooms = new List<Room>();

            List<RoomType> specialRoomTypes = RoomTypeHelper.GetSpecialRoomTypes();

            for (int i = 0; i < filledRoomIndexes.Count; i++)
            {
                Vector2Int pos = filledRoomIndexes[i];
                int distanceBetweenDungeonStart = FindDistanceFromStart(rooms, rooms[pos.x, pos.y], filledRoomIndexes);

                if (distanceBetweenDungeonStart > 1 && distanceBetweenDungeonStart < 5)
                {
                    possibleSpecialRooms.Add(rooms[pos.x, pos.y]);
                }
            }

            if (numberOfSpecialRooms > possibleSpecialRooms.Count)
            {
                numberOfSpecialRooms = possibleSpecialRooms.Count;
            }

            for (int i = 0; i < numberOfSpecialRooms; i++)
            {
                Room specialRoom = possibleSpecialRooms.ReturnRandomElement();
                RoomType specialRoomType = specialRoomTypes.ReturnRandomElement();

                if(specialRoom.roomType == RoomType.NORMAL_ROOM)
                {
                    specialRoom.roomType = specialRoomType;
                    possibleSpecialRooms.Remove(specialRoom);
                }
            }
        }



        private static int FindDistanceFromStart(Room[,] rooms, Room room, List<Vector2Int> filledRoomIndexes)
        {
            int distance = 0;

            for (int i = 0; i < filledRoomIndexes.Count; i++)
            {
                Vector2Int pos = filledRoomIndexes[i];
                rooms[pos.x, pos.y].isVisited = true;

                foreach (Vector2Int direction in Direction2D.cardinalDirectionsList)
                {
                    Vector2Int neighbourPosition = pos + direction;

                    bool canTraverse = ! (
                       neighbourPosition.x >= rooms.GetLength(0) || neighbourPosition.y >= rooms.GetLength(1) ||
                       neighbourPosition.x < 0 || neighbourPosition.y < 0 ||
                       rooms[neighbourPosition.x, neighbourPosition.y] == null ||
                       rooms[neighbourPosition.x, neighbourPosition.y].isVisited ||
                       !rooms[pos.x, pos.y].GetNeighbourRooms().Contains(rooms[neighbourPosition.x, neighbourPosition.y])
                       );

                    if (canTraverse)
                    {
                        rooms[neighbourPosition.x, neighbourPosition.y].length = rooms[pos.x, pos.y].length + 1;
                    }
                }

                if (rooms[pos.x, pos.y].Equals(room))
                {
                    distance = room.length > distance ? room.length : distance;
                }
            }
            ClearRoomTravelStatus(rooms, filledRoomIndexes);
            return distance;
        }

        private static Room FindFurthestRoom(Room[,] rooms, Vector2Int roomPosition, List<Vector2Int> filledRoomIndexes)
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

                    bool canTraverse = !(
                       neighbourPosition.x >= rooms.GetLength(0) || neighbourPosition.y >= rooms.GetLength(1) ||
                       neighbourPosition.x < 0 || neighbourPosition.y < 0 ||
                       rooms[neighbourPosition.x, neighbourPosition.y] == null ||
                       rooms[neighbourPosition.x, neighbourPosition.y].isVisited ||
                       !rooms[pos.x, pos.y].GetNeighbourRooms().Contains(rooms[neighbourPosition.x, neighbourPosition.y])
                       );

                    if (canTraverse)
                    {
                        rooms[neighbourPosition.x, neighbourPosition.y].length = rooms[pos.x, pos.y].length + 1;
                    }

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

