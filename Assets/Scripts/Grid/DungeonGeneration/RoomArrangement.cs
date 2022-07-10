using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Grid.DungeonGeneration
{
    public static class RoomArrangement
    {
        public static void ArrangeRooms(Room[,] rooms, Vector2Int dungeonEntrancePosition, List<Vector2Int> filledRoomIndexes)
        {

        }


        public static Room FindFarestRoom(Room[,] rooms, Vector2Int startPosition, List<Vector2Int> filledRoomIndexes)
        {
            Vector2Int pos = startPosition;
            Room farestRoom = rooms[pos.x, pos.y];

            int maxDistace = 0;

            for (int i = 0; i < filledRoomIndexes.Count; i++)
            {
                pos = filledRoomIndexes[i];
                rooms[pos.x, pos.y].isVisited = true;
                foreach (Vector2Int direction in Direction2D.cardinalDirectionsList)
                {
                    Vector2Int neighbourPosition = pos + direction;

                    if (neighbourPosition.x >= rooms.GetLength(0) || neighbourPosition.y >= rooms.GetLength(1) ||
                       neighbourPosition.x < 0 || neighbourPosition.y < 0 ||
                       rooms[neighbourPosition.x, neighbourPosition.y] == null ||
                       rooms[neighbourPosition.x, neighbourPosition.y].isVisited ||
                       ! rooms[pos.x, pos.y].GetNeighbourRooms().Contains(rooms[neighbourPosition.x, neighbourPosition.y])
                       )
                    {
                        continue;
                    }
                    rooms[neighbourPosition.x, neighbourPosition.y].length = rooms[pos.x, pos.y].length + 1;
                    
                }

                if (maxDistace < rooms[pos.x, pos.y].length)
                {
                    farestRoom = rooms[pos.x, pos.y];
                    maxDistace = rooms[pos.x, pos.y].length;
                }
            }
           
            ClearRoomTravelStatus(rooms, filledRoomIndexes);

            return farestRoom;
        }

        private static void DebugList(Room[,] rooms)
        {
            string log = "";
            for (int i = rooms.GetLength(0)-1; i >= 0; i--)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    if(rooms[j,i] == null)
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

        private static void ClearRoomTravelStatus(Room[,] rooms, List<Vector2Int> filledRoomIndexes)
        {
            for (int i = 0; i < filledRoomIndexes.Count; i++)
            {
                rooms[filledRoomIndexes[i].x, filledRoomIndexes[i].y].ClearTravelStatus();
            }
        }

        public static void FindFarestRoom(Room room)
        {
            Queue<Room> notExpandedRooms = new Queue<Room>();
            List<Room> expandedRooms = new List<Room>();

            notExpandedRooms.Enqueue(room);

            while(notExpandedRooms.Count < 0)
            {

            }

        }
    }
}

