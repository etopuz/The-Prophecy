using System;
using System.Collections;
using TheProphecy.Grid;
using UnityEngine;

namespace TheProphecy.Enemy
{
    public class EnemyMovementAI : MonoBehaviour
    {
        [SerializeField] private Transform targetLeft;
        [SerializeField] private Transform targetRight;
        private CustomGrid _grid;

        private Pathfinding _pathfinding;

        Vector3 oldTargetPosition;

        private const float _PATH_UPDATE_TIME = 0.07f;
        private float _pathUpdateTimer = 0f;

        private int _currentCheckPointIndex = 0;
        private float _speed = 3f;


        void Start()
        {
            _pathfinding = GetComponent<Pathfinding>();
            _pathfinding.FindPath(transform.position, targetLeft.position);
            oldTargetPosition = targetLeft.position;
            _grid = _pathfinding.Grid;
        }

        private void Update()
        {
            UpdatePath();
        }

        private void FixedUpdate()
        {
            FollowPath();
        }

        private Vector3 ChooseTargetPivotOfCharacter()
        {
            Node targetNodeLeft = _grid.NodeFromWorldPoint(targetLeft.position);
            Node targetNodeRight = _grid.NodeFromWorldPoint(targetRight.position);

            if (targetNodeLeft.walkable)
            {
                return targetLeft.position;
            }

            return targetRight.position;

        }

        private void UpdatePath()
        {
            Vector3 target = ChooseTargetPivotOfCharacter();

            if (_pathUpdateTimer < _PATH_UPDATE_TIME)
            {
                _pathUpdateTimer += Time.deltaTime;
            }

            else
            {
                _pathUpdateTimer = 0f;

                
                Node targetNode = _grid.NodeFromWorldPoint(target);
                Node oldTargetNode = _grid.NodeFromWorldPoint(oldTargetPosition);

                if (!(targetNode.Equals(oldTargetNode)))
                {
                    _pathfinding.FindPath(transform.position, target);
                    oldTargetPosition = target;
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

    }

    
}
