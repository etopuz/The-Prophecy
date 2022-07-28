using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _offset;
    private Vector3 _velocity;

    private void Start()
    {
        _velocity = Vector3.zero;
        _offset = Vector3.forward * (-10);
        transform.position = _target.position + _offset;
    }


    private void FixedUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, Time.fixedDeltaTime);
    }
}
