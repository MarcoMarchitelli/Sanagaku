using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Deirin.AI
{
    public class Patrol : MonoBehaviour
    {
        #region Inspector Variables

        public Transform path;
        public bool startOnAwake = true;

        public float speed = 5f;
        public float waitTime = 1f;
        public float rotationAnglePerSecond = 90f;

        public bool rotatesToWaypoint = true;

        public UnityEvent OnMovementStart, OnMovementEnd, OnWaypointReached, OnPathFinished;

        #endregion

        #region Properties

        bool isMoving;

        public bool IsMoving
        {
            get { return isMoving; }
            private set
            {
                if (value != isMoving && value)
                    OnMovementStart.Invoke();
                else if (value != isMoving)
                    OnMovementEnd.Invoke();
                isMoving = value;
            }
        }

        #endregion

        #region MonoBehaviour methods

        private void Awake()
        {
            if (!path)
                Debug.LogError(name + " does not have a path referenced!");
        }

        void Start()
        {
            if (startOnAwake)
                StartPatrol();
        }

        #endregion

        #region API

        public void StartPatrol()
        {
            StartCoroutine(FollowPath());
        }

        public void StopPatrol()
        {
            StopCoroutine(FollowPath());
        }

        #endregion

        #region Patrol mathods

        IEnumerator FollowPath()
        {
            Vector3[] wayPoints = new Vector3[path.childCount];

            for (int i = 0; i < wayPoints.Length; i++)
            {
                wayPoints[i] = path.GetChild(i).position;
                wayPoints[i] = new Vector3(wayPoints[i].x, transform.position.y, wayPoints[i].z);
            }

            transform.position = wayPoints[0];
            int nextPointIndex = 1;
            Vector3 nextPoint = wayPoints[nextPointIndex];
            transform.LookAt(nextPoint);

            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
                IsMoving = true;
                if (transform.position == nextPoint)
                {
                    nextPointIndex = (nextPointIndex + 1) % wayPoints.Length;
                    nextPoint = wayPoints[nextPointIndex];
                    IsMoving = false;

                    //events
                    if (nextPointIndex == wayPoints.Length - 1)
                        OnPathFinished.Invoke();
                    else
                        OnWaypointReached.Invoke();

                    if (rotatesToWaypoint)
                        yield return StartCoroutine(RotateTo(nextPoint));
                    else
                        yield return new WaitForSeconds(waitTime);
                }
                yield return null;
            }
        }

        IEnumerator RotateTo(Vector3 _rotationTarget)
        {
            Vector3 directionToTarget = (_rotationTarget - transform.position).normalized;
            float targetAngle = 90 - Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;

            while (Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle) > 0.05f || Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle) < -0.05f)
            {
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationAnglePerSecond);
                transform.eulerAngles = Vector3.up * angle;
                yield return null;
            }
        }

        #endregion

    }
}
