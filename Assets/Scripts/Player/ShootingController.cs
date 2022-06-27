using System.Collections;
using UnityEngine;
using TheProphecy.Player;

namespace TheProphecy.Player
{
    public class ShootingController : MonoBehaviour
    {
        public static ObjectPool _pool;
        private MovementController _movementController;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _projectileContainer;

        [SerializeField] private float _bulletSpeed = 25f;
        

        // private float _bulletSize = 1f;
        // private int _bulletCount = 1;

        void Start()
        {
            _pool = new ObjectPool(_bulletPrefab, _projectileContainer);
            _pool.FillThePool(30);
            _movementController = GetComponent<MovementController>();
        }

        public void ShootProjectile()
        {
            
            GameObject bullet = _pool.GetFromPool();
            Rigidbody2D bullet_Rb = bullet.GetComponent<Rigidbody2D>();

            float directionAngle = Vector2.SignedAngle(new Vector2(1, 0), _movementController.Direction);
            bullet.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
            bullet.transform.position = transform.position;

            bullet_Rb.velocity = _movementController.Direction * _bulletSpeed;
        }

    }
}

