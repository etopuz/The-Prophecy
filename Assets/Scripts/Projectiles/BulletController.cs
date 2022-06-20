using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheProphecy.Enemy;

namespace TheProphecy.Projectiles
{
    public class BulletController : MonoBehaviour
    {
        private int _damage = 1;
        private Rigidbody2D _bulletRigidbody;


        private void Start()
        {
            _bulletRigidbody = GetComponent<Rigidbody2D>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(TagLayerData.ENEMY))
            {
                EnemyHits enemyHits = collision.gameObject.GetComponent<EnemyHits>();
                enemyHits.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
            // TODO: else if obstacle
        }
    }
}
