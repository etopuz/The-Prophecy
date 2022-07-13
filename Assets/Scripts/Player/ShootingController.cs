using UnityEngine;
using TheProphecy.Projectiles;

namespace TheProphecy.Player
{
    public class ShootingController : MonoBehaviour
    {

        public static ObjectPool _pool;
        private Vector2 _direction = new Vector2(1, 0);

        [Header("References")]
        [SerializeField] private Joystick _aimJoystick;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _projectileContainer;
        [SerializeField] private GameObject _center;
        [SerializeField] private GameObject _bow;



        [Header("Bullet Stats")]
        [SerializeField] private float _bulletSpeed = 15f;
        private const float FIRE_COOLDOWN_TIME = 0.3f;
        private float _fireCooldownTimer = 0f;
        private bool _isFireOnCooldown = false;
        // private float _bulletSize = 1f;
        // private int _bulletCount = 1;

        void Start()
        {
            _pool = new ObjectPool(_bulletPrefab, _projectileContainer);
            _pool.FillThePool(30);
        }

        private void Update()
        {
            CalculateFireCooldown();
        }

        private void FixedUpdate()
        {
            ShootProjectile();
        }

        public void ShootProjectile()
        {
            int bulletInitialDegree = -90;
            if (_aimJoystick.Horizontal != 0 || _aimJoystick.Vertical != 0)
            {
                _direction = _aimJoystick.Direction;
                float directionAngle = Vector2.SignedAngle(new Vector2(1, 0), _direction);
                _center.transform.rotation = Quaternion.Euler(0, 0, directionAngle);

                if (!_isFireOnCooldown)
                {
                    GameObject bullet = _pool.GetFromPool();
                    Bullet bulletScript = bullet.GetComponent<Bullet>();

                    bulletScript.FireAndMove(_bow.transform.position, _direction.normalized, directionAngle + bulletInitialDegree, _bulletSpeed);

                    _isFireOnCooldown = true;
                }
            }

        }

        private void CalculateFireCooldown()
        {
            if (_isFireOnCooldown)
            {
                if (_fireCooldownTimer > FIRE_COOLDOWN_TIME)
                {
                    _fireCooldownTimer = 0f;
                    _isFireOnCooldown = false;
                }

                else
                {
                    _fireCooldownTimer += Time.deltaTime;
                }
            }
        }
    }
}

