using System.Collections;
using UnityEngine;
using TheProphecy.PlayerMovement;

namespace TheProphecy.PlayerCombat
{
    public class ShootingController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _bulletPrefab;
        private ObjectPool _pool;
        private MovementController _movementController;

        [SerializeField]
        private float _bulletSpeed = 25f;
        private float _bulletSize = 1f;
        private float _bulletLifeTime = 2.2f;
        private int _bulletCount = 1;

        void Start()
        {
            _pool = new ObjectPool(_bulletPrefab);
            _pool.FillThePool(300);
            _movementController = GetComponent<MovementController>();
        }

        public void ShootProjectile()
        {
            
            GameObject bullet = _pool.GetFromPool();
            Rigidbody2D bullet_Rb = bullet.GetComponent<Rigidbody2D>();

            float directionAngle = Vector2.SignedAngle(new Vector2(1, 0), _movementController.direction);
            bullet.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
            bullet.transform.position = transform.position;

            bullet_Rb.velocity = _movementController.direction * _bulletSpeed;


            StartCoroutine(ReturnBullet(bullet));
        }

        private IEnumerator ReturnBullet(GameObject bullet)
        {
 
                yield return new WaitForSeconds(_bulletLifeTime);

                _pool.AddToPool(bullet);
        }

    }
}

