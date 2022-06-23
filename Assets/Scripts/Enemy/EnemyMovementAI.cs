using System;
using System.Collections;
using UnityEngine;

namespace TheProphecy.Enemy
{
    public class EnemyMovementAI : MonoBehaviour
    {
        const float minPathUpdateTime = 0.3f;
        const float pathUpdateMoveThreshold = 0.5f;

        public Transform target;
        Vector3 moveDirection;

        public float speed = 100;
        public float turnSpeed = 3;
        public float turnDst = 5;
        public float stoppingDst = 10;

        Path path;

        void Start()
        {
            StartCoroutine(UpdatePath());
        }

        IEnumerator UpdatePath()
        {

            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

            float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
            Vector3 targetPosOld = target.position;

            while (true)
            {
                yield return new WaitForSeconds(minPathUpdateTime);
                if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
                {
                    PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                    targetPosOld = target.position;
                }
            }
        }

        public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                path = new Path(waypoints, transform.position, turnDst, stoppingDst);

                
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }


        IEnumerator FollowPath()
        {
            bool followingPath = true;
            int pathIndex = 0;
            float speedPercent = 1;

            while (followingPath)
            {
                Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
                while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
                {
                    if (pathIndex == path.finishLineIndex)
                    {
                        followingPath = false;
                        break;
                    }

                    else
                    {
                        pathIndex++;
                    }
                }

                if (followingPath)
                {

                    if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                    {
                        speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                        if (speedPercent < 0.01f)
                        {
                            followingPath = false;
                        }
                    }

                    moveDirection = (path.lookPoints[pathIndex] - transform.position).normalized;

                    Debug.DrawLine(transform.position, transform.position + moveDirection);

                    transform.Translate(moveDirection * Time.deltaTime * speed * speedPercent, Space.Self);
                }

                yield return null;

            }

        }


        private void OnDrawGizmos()
        {

            if (path != null)
            {
                path.DrawWithGizmos();
            }
        }
    }

    
}
