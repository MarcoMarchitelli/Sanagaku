using UnityEngine.Events;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisce il rimablzo dell'orb
    /// </summary>
    public class OrbBounceBehaviour : BaseBehaviour
    {
        #region Events
        /// <summary>
        /// Evento lanciato al bounce della pallina
        /// </summary>
        [SerializeField] protected UnityVector3Event OnBounce;
        /// <summary>
        /// Evento lanciato alla collisione con un DamageReceiver
        /// </summary>
        [SerializeField] protected UnityDamageReceiverEvent OnDamageReceiverHit;
        /// <summary>
        /// Evento lanciato alla collisione con un elemento che distrugge l'orb
        /// </summary>
        [SerializeField] protected UnityEvent OnDestroyHit;
        #endregion

        /// <summary>
        /// Sets the transform rotation to the new rotation given by the bounce.
        /// </summary>
        /// <param name="_direction">The direction of the body when bouncing.</param>
        /// <param name="_normal">The normal of the hit surface.</param>
        /// <returns></returns>
        protected virtual void Bounce(Vector3 _direction, Vector3 _normal)
        {
            Vector3 bounceDirection = Vector3.Reflect(_direction, _normal);
            float rot = 90 - Mathf.Atan2(bounceDirection.z, bounceDirection.x) * Mathf.Rad2Deg;
            Vector3 newDirection = new Vector3(0, rot, 0);
            OnBounce.Invoke(newDirection);
        }

        /// <summary>
        /// Handles the orb behaviours depending on the BounceOnBehaviour hit.
        /// </summary>
        /// <param name="_b">the BounceOnBehaviour hit.</param>
        /// <param name="_hitNormal">the normal of the contact point.</param>
        protected void HandleBounceOnBehaviour(BounceOn _b, Vector3 _hitNormal)
        {
            switch (_b.BehaviourType)
            {
                case BounceOn.BounceType.Ignore:
                    break;
                case BounceOn.BounceType.Realistic:
                    Bounce(transform.forward, _hitNormal);
                    break;
                case BounceOn.BounceType.Destroy:
                    OnDestroyHit.Invoke();
                    break;
            }
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (!IsSetupped)
                return;

            BounceOn _b = collision.collider.GetComponent<BounceOn>();
            if (_b)
                HandleBounceOnBehaviour(_b, collision.contacts[0].normal);
            else
                Bounce(transform.forward, collision.contacts[0].normal);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!IsSetupped)
                return;

            DamageReceiverBehaviour _drb = other.GetComponent<DamageReceiverBehaviour>();
            if (_drb && !_drb.Entity.GetType().IsAssignableFrom(typeof(PlayerController))) // controllo che non sia un player
            {
                OnDamageReceiverHit.Invoke(_drb);
            }
        }
    }
}