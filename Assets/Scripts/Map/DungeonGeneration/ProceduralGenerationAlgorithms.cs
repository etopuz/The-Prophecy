using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheProphecy.Map.DungeonGeneration
{
    public static class ProceduralGenerationAlgorithms
    {

        public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
        {
            HashSet<Vector2Int> path = new HashSet<Vector2Int>();
            path.Add(startPosition);
            Vector2Int previousPosition = startPosition;

            for (int i = 0; i < walkLength; i++)
            {
                Vector2Int newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
                path.Add(newPosition);
                previousPosition = newPosition;

            }
            return path;
        }

        public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
        {
            List<Vector2Int> corridor = new List<Vector2Int>();
            Vector2Int direction = Direction2D.GetRandomCardinalDirection();
            Vector2Int currentPosition = startPosition;

            for(int i = 0; i < corridorLength; i++)
            {
                currentPosition += direction;
                corridor.Add(currentPosition);  
            }

            return corridor;
        }

        public static List<Room> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
        {
            Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
            List<Room> roomsList = new List<Room>();

            roomsQueue.Enqueue(spaceToSplit);

            while (roomsQueue.Count > 0)
            {
                BoundsInt roomBounds = roomsQueue.Dequeue();
                if(roomBounds.size.y >= minHeight && roomBounds.size.x >= minWidth)
                {
                    if(Random.value < 0.5f)
                    {
                        if(roomBounds.size.y >= minHeight * 2)
                        {
                            SplitHorizontally(minHeight, roomsQueue, roomBounds);
                        }
                        
                        else if (roomBounds.size.x >= minWidth * 2)
                        {
                            SplitVertically(minWidth, roomsQueue, roomBounds);
                        }

                        else
                        {
                            roomsList.Add(new Room(roomBounds));
                        }
                    }

                    else
                    {
                        if (roomBounds.size.x >= minWidth * 2)
                        {
                            SplitVertically(minWidth, roomsQueue, roomBounds);
                        }

                        else if (roomBounds.size.y >= minHeight * 2)
                        {
                            SplitHorizontally( minHeight, roomsQueue, roomBounds);
                        }

                        else
                        {
                            roomsList.Add(new Room(roomBounds));
                        }
                    }
                }
            }

            return roomsList;
        }

        private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            var xSplit = Random.Range(1, room.size.x);
            BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
            BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
                new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }

        private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            var ySplit = Random.Range(1, room.size.y);
            BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
            BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
                new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }
    }


/// <summary>
/// Start from up goes clockwise
/// </summary>
    public static class Direction2D
    {
        public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1),    //UP
        new Vector2Int(1,0),    //RIGHT
        new Vector2Int(0,-1),   //DOWN
        new Vector2Int(-1,0)    //LEFT
    };

        public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

        public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP

    };


        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirectionsList.ReturnRandomElement();
        }
    }
}
