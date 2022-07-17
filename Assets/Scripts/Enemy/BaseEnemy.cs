using UnityEngine;
using TheProphecy.Interfaces;
using UnityEngine.UI;
using TheProphecy.LevelRun;
using Random = UnityEngine.Random;

namespace TheProphecy.Enemy
{
    public class BaseEnemy : MonoBehaviour, IDamageable
    {
        [Header("Health Variables")]
        [SerializeField] private const int MAX_HEALTH = 5;
        [SerializeField] private int _health = 5;

        [Header("HealthBar")]
        [SerializeField] private Canvas _healthBar;
        [SerializeField] private Image _healthBarSlider;

        [Header("Drop Variables")]
        [SerializeField] private int _minCoinDropRate = 0;
        [SerializeField] private int _maxCoinDropRate = 5;

        public void Start()
        {
            _health = MAX_HEALTH;
        }

        public void OnTakeDamage(int damage)
        {
            _health -= damage;

            if (_health < 1)
            {
                Die();
            }
                

            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            if (_healthBar.enabled == false)
            {
                _healthBar.enabled = true;
            }

            _healthBarSlider.fillAmount = Mathf.Clamp01((float)_health / (float)MAX_HEALTH );
        }

        private void Die()
        {
            gameObject.SetActive(false);
            LevelRunStats levelStats = LevelManager.instance.levelRunStats;
            levelStats.AddKill();
            levelStats.AddCoins(Random.Range(_minCoinDropRate, _maxCoinDropRate));
        }
    }
}
