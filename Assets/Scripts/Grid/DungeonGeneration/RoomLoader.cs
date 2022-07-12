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
                    SpawnAt(_basicEnemy, room.Bounds.center, _enemyContainer);
                    SpawnAt(_basicEnemy, room.Bounds.center + Vector3.left , _enemyContainer);
                    SpawnAt(_basicEnemy, room.Bounds.center + Vector3.right, _enemyContainer);
                    SpawnAt(_basicEnemy, room.Bounds.center + Vector3.up, _enemyContainer);
                    SpawnAt(_basicEnemy, room.Bounds.center + Vector3.down, _enemyContainer);
                    break;
                case RoomType.BOSS_ROOM:
                    break;
                case RoomType.POOL:
                    break;
                case RoomType.MARKET:
                    break;
            }
        }

        public void ClearBeforeGenerate()
        {
            int numberOfEnemies = _enemyContainer.transform.childCount;

            for (int i = numberOfEnemies - 1; i >= 0; i--)
            {
                DestroyImmediate(_enemyContainer.transform.GetChild(i).gameObject);
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
