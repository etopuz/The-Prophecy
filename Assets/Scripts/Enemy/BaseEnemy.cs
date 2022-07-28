using UnityEngine;
using TheProphecy.Interfaces;
using UnityEngine.UI;
using TheProphecy.LevelRun;
using Random = UnityEngine.Random;

namespace TheProphecy.Enemy
{
    public class BaseEnemy : BaseUnit, IDamageable
    {
        [Header("Drop Variables")]
        [SerializeField] private int _minCoinDropRate = 0;
        [SerializeField] private int _maxCoinDropRate = 5;

        protected override void Die()
        {
            base.Die();
            gameObject.SetActive(false);
            LevelRunStats levelStats = LevelManager.instance.levelRunStats;
            levelStats.AddKill();
            levelStats.AddCoins(Random.Range(_minCoinDropRate, _maxCoinDropRate));
        }
    }
}
