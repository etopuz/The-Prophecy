using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheProphecy.Enemy;
using TheProphecy.Player;
using TheProphecy.Interfaces;

namespace TheProphecy.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        private int _damage = 1;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void FireAndMove(Vector3 position, Vector3 direction, float angleZ, float bulletSpeed)
        {
            transform.rotation = Quaternion.Euler(0, 0, angleZ);
            transform.position = position;
            _rigidbody.velocity = direction * bulletSpeed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable iDamageable))
            {
                iDamageable.OnTakeDamage(_damage);
            }

            ShootingController._pool.AddToPool(gameObject);
        }
    }
}
