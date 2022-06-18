using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy.PlayerMovement
{
    public class MovementController : MonoBehaviour
    {
        
        [Header("References")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private GameObject _center;
        private TrailRenderer _trailRenderer;
        private SpriteRenderer _spriteRenderer;

        [Header("Basic Movement")]
        public float speed;
        public Vector2 direction = new Vector2(1, 0);
        private Vector3 _movement;


        [Header("Dashing")]
        [SerializeField] private float _dashingVelocity;
        [SerializeField] private float _dashingTime;
        [SerializeField] private float _dashingCooldown;
        private float _lastDashTime = 0f;
        private bool _isDashing = false;

        private void Start()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!_isDashing)
            {
                Move();
                RotateCharacterWhenMove();
            }

            DashController();

        }

        private void Move()
        {
            _movement.x = _joystick.Horizontal;
            _movement.y = _joystick.Vertical;

            transform.Translate(_movement * speed * Time.deltaTime);
        }

        private void RotateCharacterWhenMove()
        {
            if (_movement.x != 0 || _movement.y != 0)
            {
                direction = _joystick.Direction;

                float directionAngle = Vector2.SignedAngle(new Vector2(1, 0), direction);
                _center.transform.rotation = Quaternion.Euler(0, 0, directionAngle);

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

        public void DashWhenButtonClicked()
        {
            bool isDashOnCooldown = Time.time - _lastDashTime < _dashingCooldown;

            if (isDashOnCooldown || _isDashing)
            {
                return;
            }

            _lastDashTime = Time.time;
            _isDashing = true;
            _trailRenderer.emitting = true;
        }

        private void DashController()
        {
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
                transform.position += new Vector3(direction.x, direction.y, 0) * _dashingVelocity * Time.deltaTime;
            }
        }

        public void GetRemainingCooldownTimeAsPercentage()
        {
            Mathf.Clamp((Time.time - _lastDashTime) / _dashingCooldown, 0, 1);
        }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, transform.position + new Vector3(direction.x, direction.y, 0) * 5);
        }
    }
}
