using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Grid.DungeonGeneration
{
    [System.Serializable]
    public class Room
    {
        private BoundsInt _bounds;
        public RoomType roomType;

        private HashSet<Room> _neighbours;

        public bool isVisited = false;
        public int length = 0;


        public BoundsInt Bounds { get => _bounds; }

        public Room(BoundsInt bounds)
        {
            _bounds = bounds;
            _neighbours = new HashSet<Room>();
        }

        public void AddNeihbour(Room room)
        {
            if(room != this)
            {
                _neighbours.Add(room);
            }
        }

        public HashSet<Room> GetNeighbourRooms()
        {
            return _neighbours;
        }

        public void ClearTravelStatus()
        {
            isVisited = false;
            length = 0;
        }
    }
}
