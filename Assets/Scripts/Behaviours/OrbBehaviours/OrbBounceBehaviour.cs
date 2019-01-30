using UnityEngine.Events;
using UnityEngine;

namespace Sangaku
{
    public class OrbBounceBehaviour : MonoBehaviour, IBehaviour
    {
        #region Events
        [SerializeField] UnityVector3Event OnBounce;
        [SerializeField] UnityDamageReceiverEvent OnDamageReceiverHit;
        [SerializeField] UnityEvent OnDestroyHit;
        #endregion

        /// <summary>
        /// Riferimento all'entitià che controlla il Behaviour
        /// </summary>
        public IEntity Entity { get; private set; }
        /// <summary>
        /// True se il Behaviour è stato setuppato, false altrimenti
        /// </summary>
        public bool IsSetupped { get; private set; }

        /// <summary>
        /// Eseguo il setup del behaviour
        /// </summary>
        /// <param name="_entity"></param>
        public void Setup(IEntity _entity)
        {
            Entity = _entity;
            IsSetupped = true;
        }

        [SerializeField] LayerMask bounceLayer;

        int bounces;
        float moveSpeed;

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
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, transform.localScale.x * .5f, out hit, Time.deltaTime * moveSpeed + .1f, bounceLayer))
            {
                DamageReceiverBehaviour drHit = hit.collider.GetComponent<DamageReceiverBehaviour>();
                if (drHit)
                {
                    OnDamageReceiverHit.Invoke(drHit);
                }

                BounceOnBehaviour _b = hit.collider.GetComponent<BounceOnBehaviour>();
                if (_b)
                    HandleBounceOnBehaviour(_b, hit.normal);
            }
        }

        public void SetMoveSpeed(float _value)
        {
            moveSpeed = _value;
        }

    }
}