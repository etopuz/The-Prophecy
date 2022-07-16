using UnityEngine;
using TheProphecy.Interfaces;
using UnityEngine.UI;
using TheProphecy;

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

        public void Start()
        {
            _health = MAX_HEALTH;
        }

        public void OnTakeDamage(int damage)
        {
            _health -= damage;

            if (_health < 1)
                gameObject.SetActive(false);

            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            if (_healthBar.enabled == false)
            {
                _healthBar.enabled = true;
            }

            _healthBarSlider.fillAmount = Mathf.Clamp01((float) (_health + 0.1f) / (float)MAX_HEALTH );
        }
    }
}
