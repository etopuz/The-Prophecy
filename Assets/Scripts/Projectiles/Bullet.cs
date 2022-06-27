using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheProphecy.Enemy;
using TheProphecy.Player;

namespace TheProphecy.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        private int _damage = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<BaseEnemy>(out BaseEnemy baseEnemy))
            {
               baseEnemy.TakeDamage(_damage);
               
            }

            ShootingController._pool.AddToPool(gameObject);
        }
    }
}
