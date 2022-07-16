using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheProphecy.Map.DungeonGeneration
{
    public class RandomWalkDungeonGenerator : SimpleRandomWalkDungeonCreator
    {
        [SerializeField] private int _roomPartitionEdgeLength = 4;
        [SerializeField] private int _dungeonEdgeLength  = 20;
        [SerializeField] private int _targetRoomCount = 13;
        [SerializeField] [Range(0, 10)] private int _offset = 1;
        [SerializeField] [Range(0f, 1f)] private float _specialRoomRatio = 0.2f;

        [SerializeField] private RoomLoader _roomLoader;

        private int _numberOfRoomsCreated = 1;

        private Room[,] _rooms;

        protected override void RunProceduralGeneration()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {
            RandomWalkDungeonGeneration(startPosition, _roomPartitionEdgeLength, _dungeonEdgeLength, _targetRoomCount);
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

            _roomLoader.ClearBeforeGenerate();

            for (int i = 0; i < _rooms.GetLength(0); i++)
            {
                for (int j = 0; j < _rooms.GetLength(1); j++)
                {
                    if (_rooms[i,j] != null)
                    {
                        HashSet<Vector2Int> roomFloors = CreateRandomWalkRoom(_rooms[i,j]);
                        floor.UnionWith(roomFloors);

                        HashSet<Vector2Int> corridorFloors = ConnectRooms(_rooms[i,j]);
                        floor.UnionWith(corridorFloors);

                        _roomLoader.SetupRoom(_rooms[i, j]);
                    }
                }
            }

            tilemapVisualizer.PaintFloorTiles(floor);
            WallGenerator.CreateWalls(floor, tilemapVisualizer);


        }

        private HashSet<Vector2Int> ConnectRooms(Room room)
        {
            HashSet<Room> neighbours = room.GetNeighbourRooms();
            HashSet<Vector2Int> corridorFloors = new HashSet<Vector2Int>();
            Vector2Int position = new Vector2Int(Mathf.FloorToInt(room.Bounds.center.x), Mathf.FloorToInt(room.Bounds.center.y));

            foreach (Room neighbour in neighbours)
            {
                Vector2Int destination = new Vector2Int(Mathf.FloorToInt(neighbour.Bounds.center.x), Mathf.FloorToInt(neighbour.Bounds.center.y));

                if((position.x - destination.x) == 0 || (position.y - destination.y) == 0)
                {
                 
                    while (position.y != destination.y)
                    {
                        if (destination.y > position.y)
                        {
                            position += Vector2Int.up;
                        }

                        else if (destination.y < position.y)
                        {
                            position += Vector2Int.down;
                        }

                        corridorFloors.Add(position);
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

                        corridorFloors.Add(position);
                    }
                }

            }

            return corridorFloors;
        }

        private HashSet<Vector2Int> CreateSimpleRoom(Room room)
        {
            HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>();

            for (int col = _offset; col < room.Bounds.size.x - _offset; col++)
            {
                for (int row = _offset; row < room.Bounds.size.y - _offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.Bounds.min + new Vector2Int(col, row);
                    roomFloor.Add(position);
                }
            }

            return roomFloor;
        }

        private HashSet<Vector2Int> CreateRandomWalkRoom(Room room)
        {
            HashSet<Vector2Int> roomFloors = new HashSet<Vector2Int>();
            BoundsInt roomBounds = room.Bounds;
            Vector2Int roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            HashSet<Vector2Int> rawRandomWalkFloor = RunRandomWalk(randomWalkParameters, roomCenter);

            foreach (Vector2Int position in rawRandomWalkFloor)
            {
                bool isWalkInRoomBounds = position.x >= (roomBounds.xMin + _offset) &&
                    position.x <= (roomBounds.xMax - _offset) &&
                    position.y >= (roomBounds.yMin + _offset) &&
                    position.y <= (roomBounds.yMax - _offset);

                if (isWalkInRoomBounds)
                {
                    roomFloors.Add(position);
                }

            }
            return roomFloors;
        }

        private void RandomWalkDungeonGeneration(Vector2Int startPosition, int roomSize, int dungeonSize, int numberOfGoalRooms)
        {

            int maxRoomInEdge = dungeonSize / roomSize;
            int middle = maxRoomInEdge / 2;
            _rooms = new Room[maxRoomInEdge, maxRoomInEdge];

            Queue<Vector2Int> indexesOfEndNodeRooms = new Queue<Vector2Int>();
            List<Vector2Int> filledRoomIndexes = new List<Vector2Int>();

            Vector2Int dungeonEntrancePos = new Vector2Int(1, 1);

            indexesOfEndNodeRooms.Enqueue(new Vector2Int(dungeonEntrancePos.x, dungeonEntrancePos.y));
            filledRoomIndexes.Add(new Vector2Int(dungeonEntrancePos.x, dungeonEntrancePos.y));

            _rooms[dungeonEntrancePos.x, dungeonEntrancePos.y] = new Room(
                new BoundsInt(GetCalculatedWorldPosition(startPosition, 1, 1),
                new Vector3Int(roomSize, roomSize, 0))
            );

            if (numberOfGoalRooms > Mathf.Pow(maxRoomInEdge, 2))
            {
                numberOfGoalRooms = (int)(Mathf.Pow(middle, 2));
            }

            _numberOfRoomsCreated = 1;

            while (_numberOfRoomsCreated < numberOfGoalRooms)
            {

                if (indexesOfEndNodeRooms.Count == 0 && filledRoomIndexes.Count>0)
                {
                    Vector2Int randomIndexes = filledRoomIndexes.ReturnRandomElement();
                    indexesOfEndNodeRooms.Enqueue(randomIndexes);
                }


                Vector2Int currentIndexes = indexesOfEndNodeRooms.Dequeue();
                Room currentRoom = _rooms[currentIndexes.x, currentIndexes.y];


                int rangeMultiplier = Mathf.FloorToInt((1 - (float)_numberOfRoomsCreated / (float)numberOfGoalRooms) * 4);

                int numberOfRoomsToGenerate = Random.Range(rangeMultiplier, 5);

                while (numberOfRoomsToGenerate > 0)
                {
                    Vector2Int nextPossibleRoomPos = Direction2D.GetRandomCardinalDirection() + currentIndexes;

                    bool isPossibleToCreateRoomInThatDirection = maxRoomInEdge > nextPossibleRoomPos.x && maxRoomInEdge > nextPossibleRoomPos.y &&
                        0 <= nextPossibleRoomPos.x && 0 <= nextPossibleRoomPos.y &&
                        _numberOfRoomsCreated < numberOfGoalRooms &&
                        _rooms[nextPossibleRoomPos.x, nextPossibleRoomPos.y] == null;


                    if (isPossibleToCreateRoomInThatDirection)
                    {
                        Room roomToPass = new Room(
                            new BoundsInt(GetCalculatedWorldPosition(startPosition, nextPossibleRoomPos.x, nextPossibleRoomPos.y),
                            new Vector3Int(roomSize, roomSize, 0))
                        );

                        _rooms[nextPossibleRoomPos.x, nextPossibleRoomPos.y] = roomToPass;

                        currentRoom.AddNeihbour(roomToPass);
                        roomToPass.AddNeihbour(currentRoom);

                        indexesOfEndNodeRooms.Enqueue(nextPossibleRoomPos);
                        filledRoomIndexes.Add(nextPossibleRoomPos);

                        _numberOfRoomsCreated++;

                    }
                    numberOfRoomsToGenerate--;
                }
            }


            RoomArrangement.SetRoomTypes(_rooms, dungeonEntrancePos, filledRoomIndexes, _specialRoomRatio);

            
        }

        private Vector3Int GetCalculatedWorldPosition(Vector2Int position, int xOffset, int yOffset)
        {
            return new Vector3Int(position.x + (xOffset * _roomPartitionEdgeLength), position.y + (yOffset * _roomPartitionEdgeLength), 0);
        }

        public void OnDrawGizmos()
        {

            if (_rooms == null)
                return;

            for (int i = 0; i < _rooms.GetLength(0); i++)
            {
                for (int j = 0; j < _rooms.GetLength(1); j++)
                {
                    if(_rooms[i,j] != null)
                    {
                        Room room = _rooms[i, j];
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

        public float GetRoomRadius()
        {
            float radius = (_roomPartitionEdgeLength - 2) / 2;
            return Mathf.Sqrt(Mathf.Pow(radius, 2) * 2);
        }

    }
}

