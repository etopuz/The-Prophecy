using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.1f;

    private void FixedUpdate()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, _target.position, _smoothSpeed);
        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;
    }
}
