using System;
using System.Collections;
using TheProphecy.Grid;
using UnityEngine;

namespace TheProphecy.Enemy
{
    public class EnemyMovementAI : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Pathfinding _pathfinding;

        Vector3 oldTargetPosition;

        private const float _PATH_UPDATE_TIME = 0.07f;
        private float _pathUpdateTimer = 0f;

        private int _currentCheckPointIndex = 0;
        private float _speed = 10;

        void Awake()
        {
            _pathfinding = GetComponent<Pathfinding>();
        }

        void Start()
        {
            _pathfinding.FindPath(transform.position, target.position);
            oldTargetPosition = target.position;
        }

        private void Update()
        {
            UpdatePath();
        }

        private void FixedUpdate()
        {
            FollowPath();
        }

        private void UpdatePath()
        {
            if (_pathUpdateTimer < _PATH_UPDATE_TIME)
            {
                _pathUpdateTimer += Time.deltaTime;
            }

            else
            {
                _pathUpdateTimer = 0f;

                CustomGrid grid = _pathfinding.Grid;
                Node targetNode = grid.NodeFromWorldPoint(target.position);
                Node oldTargetNode = grid.NodeFromWorldPoint(oldTargetPosition);

                if (!(targetNode.Equals(oldTargetNode)))
                {
                    _pathfinding.FindPath(transform.position, target.position);
                    oldTargetPosition = target.position;
                    _currentCheckPointIndex = 0;
                }
            }
        }

        private void FollowPath()
        {
            if(_currentCheckPointIndex < _pathfinding.waypoints.Length)
            {
                CustomGrid grid = _pathfinding.Grid;
                Node currentTransformNode = grid.NodeFromWorldPoint(transform.position);
                Node nextWaypointNode = grid.NodeFromWorldPoint(_pathfinding.waypoints[_currentCheckPointIndex]);

                if ((currentTransformNode.Equals(nextWaypointNode))){
                    _currentCheckPointIndex++;
                }

                if (_currentCheckPointIndex < _pathfinding.waypoints.Length)
                {
                    Vector3 moveDirection = (_pathfinding.waypoints[_currentCheckPointIndex] - transform.position).normalized;
                    transform.Translate(moveDirection * Time.deltaTime * _speed);
                }

            }
        }


        private void OnDrawGizmos()
        {
            _pathfinding.OnDrawGizmos();
        }
    }

    
}
