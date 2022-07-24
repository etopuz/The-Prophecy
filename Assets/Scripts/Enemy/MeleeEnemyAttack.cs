using System.Collections;
using System.Collections.Generic;
using TheProphecy.Interfaces;
using UnityEngine;

namespace TheProphecy.Enemy
{
    public class MeleeEnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] [Range(0f, 1f)] private const float _DAMAGE_DELAY = 0.5f;
        private float _damageDelayCounter = 0f;
        private bool _isAttacking = false;

        private void Update()
        {
            _damageDelayCounter += Time.deltaTime;

            if (_damageDelayCounter > _DAMAGE_DELAY)
            {
                _damageDelayCounter = 0;
                _isAttacking = true;
            }

            else
            {
                _isAttacking = false;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.parent.TryGetComponent<IDamageable>(out IDamageable iDamageable) 
                && _isAttacking )
            {
                iDamageable.OnTakeDamage(_damage);
            }
        }
    }
}
