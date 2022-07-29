using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.Map.DungeonGeneration
{
    public class RoomLoader : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _chest;
        [SerializeField] private GameObject _boss;
        [SerializeField] private GameObject _basicEnemy;

        [Header("Containers")]
        [SerializeField] private GameObject _chestContainer;
        [SerializeField] private GameObject _bossContainer;
        [SerializeField] private GameObject _enemyContainer;

        public void SetupRoom(Room room)
        {
            switch (room.roomType)
            {
                case RoomType.PLAYER_SPAWN:
                    _player.transform.position = room.Bounds.center;
                    break;
                case RoomType.TREASURE_ROOM:
                    Spawn(_chest, room.Bounds.center, _chestContainer);
                    Spawn(_chest, room.Bounds.center + Vector3.left, _chestContainer);
                    Spawn(_chest, room.Bounds.center + Vector3.right, _chestContainer);
                    Spawn(_chest, room.Bounds.center + Vector3.up, _chestContainer);
                    Spawn(_chest, room.Bounds.center + Vector3.down, _chestContainer);
                    break;
                case RoomType.NORMAL_ROOM:
                    Spawn(_basicEnemy, room.Bounds.center, _enemyContainer);
                    Spawn(_basicEnemy, room.Bounds.center + Vector3.left , _enemyContainer);
                    Spawn(_basicEnemy, room.Bounds.center + Vector3.right, _enemyContainer);
                    Spawn(_basicEnemy, room.Bounds.center + Vector3.up, _enemyContainer);
                    Spawn(_basicEnemy, room.Bounds.center + Vector3.down, _enemyContainer);
                    break;
                case RoomType.BOSS_ROOM:
                    Spawn(_boss, room.Bounds.center, _bossContainer);
                    break;
            }
        }

        public void ClearBeforeGenerate()
        {
            ClearChildren(_bossContainer);
            ClearChildren(_enemyContainer);
            ClearChildren(_chestContainer);
        }

        private void ClearChildren(GameObject parent)
        {
            int numberOfChildren = parent.transform.childCount;

            for (int i = numberOfChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(parent.transform.GetChild(i).gameObject);
            }
        }

        private void Spawn(GameObject gameObject, Vector3 position, GameObject parent)
        {
            GameObject instance = Instantiate(gameObject, parent.transform);
            instance.transform.position = position;
        }

    }
}
