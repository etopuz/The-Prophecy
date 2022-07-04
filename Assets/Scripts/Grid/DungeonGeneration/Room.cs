using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Grid.DungeonGeneration
{
    public class Room
    {
        private BoundsInt _bounds;

        public RoomType roomType;
        public HashSet<Room> neighbours;
        public BoundsInt Bounds { get => _bounds; }

        public Room(BoundsInt bounds)
        {
            _bounds = bounds;
        }
    }
}
