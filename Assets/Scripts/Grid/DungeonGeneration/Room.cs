using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Grid.DungeonGeneration
{
    public class Room
    {
        private BoundsInt _bounds;

        public RoomType roomType;
        public BoundsInt Bounds { get => _bounds; }
        private HashSet<Room> neihbours;

        public Room(BoundsInt bounds)
        {
            _bounds = bounds;
            neihbours = new HashSet<Room>();
        }

        public void AddNeihbour(Room room)
        {
            if(room != this)
            {
                neihbours.Add(room);
            }
        }

        public HashSet<Room> GetNeighbourRooms()
        {
            return neihbours;
        }
    }
}
