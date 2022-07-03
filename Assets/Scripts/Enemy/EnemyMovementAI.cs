using TheProphecy.Grid;
using TheProphecy.Grid.PathFinding;
using UnityEngine;

namespace TheProphecy.Enemy
{
    public class EnemyMovementAI : MonoBehaviour
    {
        [SerializeField] private Transform targetLeft;
        [SerializeField] private Transform targetRight;
        [SerializeField] private Transform gridGameObject_;

        private CustomGrid _grid;
        private Pathfinding _pathfinding;
        private Vector3[] _waypoints;


        Vector3 oldTargetPosition;

        private const float _PATH_UPDATE_TIME = 0.07f;
        private float _pathUpdateTimer = 0f;

        private int _currentCheckPointIndex = 0;
        private float _speed = 3f;


        void Start()
        {
            _pathfinding = gridGameObject_.GetComponent<Pathfinding>();
            _waypoints = _pathfinding.FindPath(transform.position, targetLeft.position);
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
                    _waypoints = _pathfinding.FindPath(transform.position, target);
                    oldTargetPosition = target;
                    _currentCheckPointIndex = 0;
                }
            }
        }

        private void FollowPath()
        {
            if(_currentCheckPointIndex < _waypoints.Length)
            {
                Node currentTransformNode = _grid.NodeFromWorldPoint(transform.position);
                Node nextWaypointNode = _grid.NodeFromWorldPoint(_waypoints[_currentCheckPointIndex]);

                if ((currentTransformNode.Equals(nextWaypointNode))){
                    _currentCheckPointIndex++;
                }

                if (_currentCheckPointIndex < _waypoints.Length)
                {
                    Vector3 moveDirection = (_waypoints[_currentCheckPointIndex] - transform.position).normalized;
                    transform.Translate(moveDirection * Time.deltaTime * _speed);
                }

            }
        }

    }

    
}
