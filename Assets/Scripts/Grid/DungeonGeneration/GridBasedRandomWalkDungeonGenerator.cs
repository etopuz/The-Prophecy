using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheProphecy.Grid.DungeonGeneration
{
    public class GridBasedRandomWalkDungeonGenerator : SimpleRandomWalkDungeonCreator
    {
        [SerializeField] private int _roomSize = 4;
        [SerializeField] private int _dungeonSize  = 20;
        [SerializeField] private int _targetRoomCount = 13;
        [SerializeField] [Range(0, 10)] private int _offset = 1;

        Room[,] rooms;

        protected override void RunProceduralGeneration()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {
            RandomWalkDungeonGeneration(startPosition, _roomSize, _dungeonSize, _targetRoomCount);
        }


        public void RandomWalkDungeonGeneration(Vector2Int startPosition, int roomSize, int dungeonSize, int numberOfGoalRooms)
        {


            int maxRoomInEdge = dungeonSize / roomSize;
            int middle = maxRoomInEdge / 2;
            rooms = new Room[maxRoomInEdge, maxRoomInEdge];

            Queue<Vector2Int> indexesOfEndNodeRooms = new Queue<Vector2Int>();
            List<Vector2Int> indexesOfCreatedRooms = new List<Vector2Int>();

            indexesOfEndNodeRooms.Enqueue(new Vector2Int(middle, middle)); // middle

            rooms[middle, middle] = new Room(
                new BoundsInt(GetCalculatedWorldPosition(startPosition, middle, middle),
                new Vector3Int(roomSize, roomSize, 0))
            );

            if (numberOfGoalRooms > Mathf.Pow(maxRoomInEdge, 2))
            {
                numberOfGoalRooms = (int)(Mathf.Pow(middle, 2));
            }

            int numberOfRoomsCreated = 0;

            while (numberOfRoomsCreated < numberOfGoalRooms)
            {

                if (indexesOfEndNodeRooms.Count == 0 && indexesOfCreatedRooms.Count>0)
                {
                    Vector2Int randomIndexes = indexesOfCreatedRooms.ReturnRandomElement();
                    indexesOfEndNodeRooms.Enqueue(randomIndexes);
                }


                Vector2Int currentIndexes = indexesOfEndNodeRooms.Dequeue();
                Room currentRoom = rooms[currentIndexes.x, currentIndexes.y];


                int rangeMultiplier = Mathf.FloorToInt((1 - (float)numberOfRoomsCreated / (float)numberOfGoalRooms) * 4);

                int numberOfRoomsToGenerate = Random.Range(rangeMultiplier, 5);

                while (numberOfRoomsToGenerate > 0)
                {
                    Vector2Int nextPossibleRoomIndexes = Direction2D.GetRandomCardinalDirection() + currentIndexes;

                    bool isPossibleToCreateRoomInThatDirection = maxRoomInEdge > nextPossibleRoomIndexes.x && maxRoomInEdge > nextPossibleRoomIndexes.y &&
                        0 < nextPossibleRoomIndexes.x && 0 < nextPossibleRoomIndexes.y &&
                        numberOfRoomsCreated < numberOfGoalRooms &&
                        rooms[nextPossibleRoomIndexes.x, nextPossibleRoomIndexes.y] == null;


                    if (isPossibleToCreateRoomInThatDirection)
                    {
                        Room roomToPass = new Room(
                            new BoundsInt(GetCalculatedWorldPosition(startPosition, nextPossibleRoomIndexes.x, nextPossibleRoomIndexes.y),
                            new Vector3Int(roomSize, roomSize, 0))
                        );

                        rooms[nextPossibleRoomIndexes.x, nextPossibleRoomIndexes.y] = roomToPass;

                        currentRoom.AddNeihbour(roomToPass);
                        roomToPass.AddNeihbour(currentRoom);

                        indexesOfEndNodeRooms.Enqueue(nextPossibleRoomIndexes);
                        indexesOfCreatedRooms.Add(nextPossibleRoomIndexes);

                        numberOfRoomsCreated++;

                    }
                    numberOfRoomsToGenerate--;
                }
            }

            Debug.Log(indexesOfCreatedRooms.Count);

        }


       public Vector3Int GetCalculatedWorldPosition(Vector2Int position, int xOffset, int yOffset)
        {
            return new Vector3Int((position.x + xOffset * _roomSize), (position.y + yOffset * _roomSize), 0);
        }


        public void OnDrawGizmos()
        {

            if (rooms == null)
                return;

            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    if(rooms[i,j] != null)
                    {
                        Room room = rooms[i, j];
                        Gizmos.DrawSphere(room.Bounds.center + new Vector3(0,0,999), 0.5f);
                        
                        HashSet<Room> neigbours = room.GetNeighbourRooms();
                        foreach (Room n in neigbours)
                        {
                            Gizmos.DrawLine(room.Bounds.center, n.Bounds.center);
                        }
                    }
                }
            }
        }

    }
}

