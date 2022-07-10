using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Grid.DungeonGeneration
{
    public class RoomLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _player, _chest, _basicEnemy, _enemyContainer;

        public void SetupRoom(Room room)
        {
            switch (room.roomType)
            {
                case RoomType.PLAYER_SPAWN:
                    SpawnAt(_player, room.Bounds.center);
                    break;
                case RoomType.TREASURE_ROOM:
                    break;
                case RoomType.NORMAL_ROOM:
                    SpawnAt(_basicEnemy, room.Bounds.center);
                    SpawnAt(_basicEnemy, room.Bounds.center + Vector3.left);
                    SpawnAt(_basicEnemy, room.Bounds.center + Vector3.right);
                    SpawnAt(_basicEnemy, room.Bounds.center + Vector3.up);
                    SpawnAt(_basicEnemy, room.Bounds.center + Vector3.down);
                    break;
                case RoomType.BOSS_ROOM:
                    break;
                case RoomType.POOL:
                    break;
                case RoomType.MARKET:
                    break;
            }
        }

        private void SpawnAt(GameObject gameObject, Vector3 position)
        {
            if(gameObject.scene.name == null)
            {
                Instantiate(gameObject);
            }

            gameObject.transform.position = position;
        }

        private void SpawnAt(GameObject gameObject, Vector3 position, GameObject parent)
        {
            if (gameObject.scene.name == null)
            {
                Instantiate(gameObject, parent.transform);
            }

            gameObject.transform.position = position;
        }

    }
}
