using UnityEngine;
using UnityEngine.Events;

namespace Deirin.AI
{
    public class Scanner : MonoBehaviour
    {
        public enum ScanType { fieldOfView, circularArea };

        #region Inspector Variables

        public ScanType scanType;
        public bool canSeeThroughObstacles;

        public float timeToScan = 0f;
        public LayerMask obstacleLayer;
        public float scanAreaLenght = 5f;

        public float fovAngle = 45;
        public Color spotLightColor, detectedSpotlightColor;

        public float scanAreaRadius = 5f;

        public TransformEvent OnTargetSpotted, OnTargetLost;

        #endregion

        #region Private Variables

        Transform target;
        Light spotLight;
        float spotlightRangeDifference = 3f;
        float targetVisibleTimer;
        bool hasSpottedTargetOnce = false;

        #endregion

        #region MonoBehaviour methods

        private void Awake()
        {
            target = FindObjectOfType<ScanTarget>().transform;
            timeToScan = Mathf.Abs(timeToScan);
            if (timeToScan == 0) timeToScan = 0.001f;
            if (!target)
            {
                Debug.LogError("No Target object found in the scene!");
                return;
            }
            if (scanType == ScanType.fieldOfView)
            {
                spotLight = gameObject.AddComponent<Light>();
                spotLight.type = LightType.Spot;
                spotLight.range = scanAreaLenght + spotlightRangeDifference;
                spotLight.spotAngle = fovAngle;
                spotLight.color = spotLightColor;
                spotLight.intensity = 50f;
                spotLight.shadows = LightShadows.Hard;
                spotLight.shadowBias = 0f;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (scanType == ScanType.fieldOfView)
                Gizmos.DrawRay(transform.position, transform.forward * scanAreaLenght);
            else
                Gizmos.DrawWireSphere(transform.position, scanAreaRadius);
        }

        private void Update()
        {
            if (CanSeeTarget())
            {
                targetVisibleTimer += Time.deltaTime;
            }
            else
            {
                targetVisibleTimer -= Time.deltaTime;
            }

            targetVisibleTimer = Mathf.Clamp(targetVisibleTimer, 0, timeToScan);

            if (spotLight)
                spotLight.color = Color.Lerp(spotLightColor, detectedSpotlightColor, targetVisibleTimer / timeToScan);

            if (targetVisibleTimer >= timeToScan && !hasSpottedTargetOnce)
            {
                OnTargetSpotted.Invoke(target);
                hasSpottedTargetOnce = true;
            }
            if (targetVisibleTimer < timeToScan && hasSpottedTargetOnce)
            {
                OnTargetLost.Invoke(target);
                hasSpottedTargetOnce = false;
            }
        }

        #endregion

        #region API

        bool CanSeeTarget()
        {
            if (!target)
            {
                Debug.LogWarning("No target found in scene!");
                return false;
            }
            if (scanType == ScanType.fieldOfView)
            {
                if (Vector3.Distance(transform.position, target.position) < scanAreaLenght)
                {
                    Vector3 dirToTarget = (target.position - transform.position).normalized;
                    float angleBetweenScannerAndTarget = Vector3.Angle(transform.forward, dirToTarget);
                    if (angleBetweenScannerAndTarget < fovAngle / 2f)
                    {
                        if (canSeeThroughObstacles)
                            return true;
                        if (!Physics.Linecast(transform.position, target.position, obstacleLayer))
                        {
                            return true;
                        }
                    }
                }
            }
            else
            if (scanType == ScanType.circularArea)
            {
                if (Vector3.Distance(transform.position, target.position) < scanAreaRadius)
                {
                    if (canSeeThroughObstacles)
                        return true;
                    if (!Physics.Linecast(transform.position, target.position, obstacleLayer))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

    }

    [System.Serializable]
    public class TransformEvent : UnityEvent<Transform>
    {

    }
}