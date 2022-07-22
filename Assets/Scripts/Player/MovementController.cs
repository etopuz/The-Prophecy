using UnityEngine;
using TheProphecy.Interfaces;

namespace TheProphecy.Player
{
    public class MovementController : MonoBehaviour, ISkill
    {
        
        [Header("References")]
        [SerializeField] private Joystick _moveJoystick;
        private TrailRenderer _trailRenderer;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        [Header("Basic Movement")]
        public float speed;
        private Vector2 _direction = new Vector2(1, 0);
        private Vector3 _movement;


        [Header("Dashing")]
        [SerializeField] private float _dashingVelocity;
        [SerializeField] private float _dashingTime;
        [SerializeField] private float _dashingCooldown;
        private bool _isDashOnCooldown;
        private float _lastDashTime = 0f;
        private bool _isDashing = false;
        private float _dashCooldownPercentage = 1f;

        private void Start()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CalculateDashCooldownPercentage();
        }

        private void FixedUpdate()
        {
            if (!_isDashing)
            {
                Move();
                RotateCharacterWhenMove();
            }

            Dash();
        }

        private void Move()
        {
            _movement.x = _moveJoystick.Horizontal;
            _movement.y = _moveJoystick.Vertical;

            //transform.Translate(_movement * speed * Time.deltaTime);
            _rigidbody.MovePosition(transform.position + (_movement * speed * Time.deltaTime));
        }

        private void RotateCharacterWhenMove()
        {
            if (_movement.x != 0 || _movement.y != 0)
            {
                _direction = _moveJoystick.Direction;

                if (_movement.x < 0)
                {
                    _spriteRenderer.flipX = true;
                }

                else if(_movement.x > 0)
                {
                    _spriteRenderer.flipX = false;
                }
            }
            
        }

        public void OnDashButtonPressed()
        {
            if (_isDashOnCooldown || _isDashing)
            {
                return;
            }

            _lastDashTime = Time.time;
            _isDashing = true;
            _trailRenderer.emitting = true;
        }

        private void Dash()
        {
            _isDashOnCooldown = _lastDashTime == 0f ? false : Time.time - _lastDashTime < _dashingCooldown;

            if (!_isDashing)
            {
                return;
            }

            bool isDashFinished = Time.time - _lastDashTime > _dashingTime;

            if (isDashFinished)
            {
                _isDashing = false;
                _trailRenderer.emitting = false;
            }

            else
            {
                _rigidbody.MovePosition(transform.position + new Vector3(_direction.x, _direction.y, 0).normalized * _dashingVelocity * Time.deltaTime);
            }
        }

        public void CalculateDashCooldownPercentage()
        {
            if (_isDashOnCooldown)
            {
                _dashCooldownPercentage = (Time.time - _lastDashTime) / _dashingCooldown;
            }
            else
            {
                _dashCooldownPercentage = 1f;
            }
        }

        public float GetCooldownPercentage()
        {
            return _dashCooldownPercentage;
        }
    }
}
