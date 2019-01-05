using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using Deirin.Utility;

namespace Deirin.AI
{
    [RequireComponent(typeof(Timer))]
    public class Shooter : MonoBehaviour
    {
        //references
        public Transform projectile;
        public Transform projectileSpawnPoint;

        //behaviours
        public bool hasTargets = true;
        public bool projectilesIgnoreObstacles = false;
        public bool searchForTargetsOnAwake = true;
        public bool startsShootingOnAwake = false;

        //parameters
        public float fireRate = 1f;
        public float projectileSpeed = 5f;
        public float projectileLifeTime = 3f;
        [Range(0.1f, 360f)]
        public float turnRateAnglesPerSecond = 90;
        public LayerMask obstacleLayer;

        public UnityEvent OnProjectileShoot;

        List<Transform> targets = new List<Transform>();
        Transform currentTarget;
        Timer timer;
        float shorterTargetDistance;

        private void Awake()
        {
            if (!projectile)
            {
                Debug.LogError(name + " has no projectile referenced!");
                return;
            }
            if (!projectileSpawnPoint)
            {
                Debug.LogError(name + " has no projectile spawn point referenced!");
                return;
            }
            if (searchForTargetsOnAwake)
            {
                FindTargets();
            }
            timer = GetComponent<Timer>();
            timer.OnTimerEnd.AddListener(Shoot);
            timer.time = fireRate;
            if (startsShootingOnAwake)
                timer.StartTimer();
        }

        private void OnDisable()
        {
            timer.OnTimerEnd.RemoveListener(Shoot);
        }

        private void Update()
        {
            if (hasTargets && targets.Count > 0)
            {
                shorterTargetDistance = Vector3.Distance(transform.position, targets[0].position);
                currentTarget = targets[0];
                if(targets.Count > 1)
                    for (int i = 1; i < targets.Count; i++)
                    {
                        float d = Vector3.Distance(transform.position, targets[i].position);
                        if (d < shorterTargetDistance)
                        {
                            shorterTargetDistance = d;
                            currentTarget = targets[i];
                        }
                    }
                Vector3 directionToTarget = (currentTarget.position - transform.position).normalized;
                float targetAngle = 90 - Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;

                while (Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle) > 0.05f || Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle) < -0.05f)
                {
                    float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * turnRateAnglesPerSecond);
                    transform.eulerAngles = Vector3.up * angle;
                }
            }
        }

        #region API

        public void SetTarget(Transform _target)
        {
            targets.Clear();
            targets.Add(_target);
            timer.StartTimer();
        }

        public void SetTargets(Transform[] _targets)
        {
            targets.Clear();
            targets = _targets.ToList();
            timer.StartTimer();
        }

        public void SetTargets(List<Transform> _targets)
        {
            targets.Clear();
            targets = _targets;
            timer.StartTimer();
        }

        public void ResetTargets()
        {
            targets.Clear();
            currentTarget = null;
            timer.StopTimer();
        }

        public void Shoot()
        {
            Transform p = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Mover m = p.GetComponent<Mover>();
            Timer t = p.GetComponent<Timer>();
            m.speed = projectileSpeed;
            m.direction = Vector3.forward;
            t.time = projectileLifeTime;
            t.StartTimer();
        }

        public void FindTargets()
        {
            List<ShootTarget> sts = new List<ShootTarget>();
            sts = FindObjectsOfType<ShootTarget>().ToList();
            foreach (ShootTarget st in sts)
            {
                targets.Add(st.transform);
            }
        }

        #endregion

    }
}
