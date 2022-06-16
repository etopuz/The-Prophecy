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
        private Rigidbody2D _rb;
        private TrailRenderer _trailRenderer;

        [Header("Basic Movement")]
        public float speed;
        public Vector2 direction = new Vector2(1, 0);
        private Vector3 _movement;


        [Header("Dashing")]
        [SerializeField] private float _dashingVelocity = 15f;
        [SerializeField] float _dashingTime = 1.5f;
        private bool _isDashing = false;
        private bool _canDash = true;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            Move();
            RotateFaceWithJoystick();

        }

        private void Move()
        {
            _movement.x = _joystick.Horizontal * speed;
            _movement.y = _joystick.Vertical * speed;

            transform.Translate(_movement * Time.deltaTime);
        }

        private void RotateFaceWithJoystick()
        {
            if (_movement.x != 0 || _movement.y != 0)
            {
                direction = _joystick.Direction;
                float directionAngle = Vector2.SignedAngle(new Vector2(1, 0), direction);
                _center.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
            }
        }

        public void Dash()
        {
            if (!_canDash)
                return;
            _canDash = false;
            _isDashing = true;
            _trailRenderer.emitting = true;
            StartCoroutine(StopDashing());

            if (_isDashing)
            {
                _rb.velocity = direction * _dashingVelocity;
                return;
            }

        }


        private IEnumerator StopDashing()
        {
            yield return new WaitForSeconds(_dashingTime);
            _trailRenderer.emitting = false;
            _isDashing = false;
            _canDash = true;
        }
    }
}
