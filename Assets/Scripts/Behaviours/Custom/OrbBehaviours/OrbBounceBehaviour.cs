using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;

namespace Sangaku
{
    public class OrbBounceBehaviour : BaseBehaviour
    {
        #region Events
        [SerializeField] UnityVector3Event OnBounce;
        [SerializeField] UnityDamageReceiverEvent OnDamageReceiverHit;
        [SerializeField] UnityEvent OnDestroyHit;
        #endregion

        protected override void CustomSetup()
        {
            sphereCastLength = transform.localScale.x * .7f;
            rayLength = sphereCastLength + transform.localScale.x * .6f;
            hitObjects = new List<GameObject>();
        }

        [SerializeField] LayerMask bounceLayer;
        [SerializeField] int collisionDetectionRaysAmount = 8;

        int bounces;
        float moveSpeed;
        float rayLength;
        float sphereCastLength;
        List<GameObject> hitObjects;

        /// <summary>
        /// Sets the transform rotation to the new rotation given by the bounce.
        /// </summary>
        /// <param name="_direction">The direction of the body when bouncing.</param>
        /// <param name="_normal">The normal of the hit surface.</param>
        /// <returns></returns>
        void Bounce(Vector3 _direction, Vector3 _normal)
        {
            Vector3 bounceDirection = Vector3.Reflect(_direction, _normal);
            float rot = 90 - Mathf.Atan2(bounceDirection.z, bounceDirection.x) * Mathf.Rad2Deg;
            Vector3 newDirection = new Vector3(0, rot, 0);
            bounces++;
            OnBounce.Invoke(newDirection);
        }

        /// <summary>
        /// Handles the orb behaviours depending on the BounceOnBehaviour hit.
        /// </summary>
        /// <param name="_b">the BounceOnBehaviour hit.</param>
        /// <param name="_hitNormal">the normal of the contact point.</param>
        void HandleBounceOnBehaviour(BounceOnBehaviour _b, Vector3 _hitNormal)
        {
            switch (_b.BehaviourType)
            {
                case BounceOnBehaviour.Type.realistic:
                    Bounce(transform.forward, _hitNormal);
                    break;
                case BounceOnBehaviour.Type.catchAndFire:
                    //?????       
                    break;
                case BounceOnBehaviour.Type.goThrough:
                    break;
                case BounceOnBehaviour.Type.destroy:
                    OnDestroyHit.Invoke();
                    break;
            }
        }

        private void Update()
        {
            RayCastsHandler(collisionDetectionRaysAmount);
            SphereCastingHandler();
        }

        public void SetMoveSpeed(float _value)
        {
            moveSpeed = _value;
        }

        void SphereCastingHandler()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, transform.localScale.x * .5f, out hit, sphereCastLength, bounceLayer))
            {
                BounceOnBehaviour _b = hit.collider.GetComponent<BounceOnBehaviour>();
                if (_b)
                    HandleBounceOnBehaviour(_b, hit.normal);
            }
        }

        void RayCastsHandler(int _raysAmount)
        {
            float angle = 360 / _raysAmount;
            Vector3 direction = transform.forward;
            hitObjects.Clear();

            for (int i = 0; i < _raysAmount; i++)
            {
                direction = Quaternion.AngleAxis(angle, transform.up) * direction;
                Ray ray = new Ray(transform.position, direction);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, direction * rayLength, Color.red);
                if (Physics.Raycast(ray, out hit, rayLength, bounceLayer))
                {
                    if (hit.collider.gameObject && !hitObjects.Contains(hit.collider.gameObject))
                    {
                        hitObjects.Add(hit.collider.gameObject);
                    }
                }
            }
            CheckHitObjects(hitObjects);
        }

        void CheckHitObjects(List<GameObject> _hitObjects)
        {
            foreach (GameObject g in _hitObjects)
            {
                PlayerOrbInteractionBehaviour _oib = g.GetComponent<PlayerOrbInteractionBehaviour>();
                if (_oib)
                {
                    _oib.CatchOrb(Entity as Orb);
                    continue;
                }

                DamageReceiverBehaviour _drb = g.GetComponent<DamageReceiverBehaviour>();
                if (_drb)
                {
                    OnDamageReceiverHit.Invoke(_drb);
                }
            }
        }

    }
}