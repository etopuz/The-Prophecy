using System.Collections;
using System.Collections.Generic;
using TheProphecy.Map.DungeonGeneration;
using UnityEngine;

namespace TheProphecy.LevelRun
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private RandomWalkDungeonGenerator levelGenerator;

        public LevelRunStats levelRunStats;

        override protected void Awake()
        {
            base.Awake();
            levelRunStats = new LevelRunStats();
            levelGenerator.GenerateDungeon();
        }

        public void ResetLevel()
        {
            levelRunStats = new LevelRunStats();
        }

    }
}
