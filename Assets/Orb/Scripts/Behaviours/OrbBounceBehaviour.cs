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
        [SerializeField] UnityEvent OnDestroyHit, OnPlayerHit;
        #endregion

        [SerializeField] LayerMask bounceLayer;
        [SerializeField] int collisionDetectionRaysAmount = 8;
        [SerializeField] float manaModifier = 2f;

        int bounces;
        int enemyHitCount;
        float moveSpeed;
        ManaBehaviour mana;

        protected override void CustomSetup()
        {
            enemyHitCount = 0f;
            mana = GetComponent<ManaBehaviour>();
        }

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

        public void SetMoveSpeed(float _value)
        {
            moveSpeed = _value;
        }

        /// <summary>
        /// Handles the mana obtained on ManaBehaviour hit.
        /// </summary>
        /// <param name="_manaAmount"></param>
        public void CatchMana(float _manaAmount)
        {
            mana.SetMana(_manaAmount + (manaModifier * enemyHitCount));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsSetupped)
                return;
            BounceOnBehaviour _b = collision.collider.GetComponent<BounceOnBehaviour>();
            if (_b)
                HandleBounceOnBehaviour(_b, collision.contacts[0].normal);
            else
            {
                Bounce(transform.forward, collision.contacts[0].normal);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsSetupped)
                return;
            PlayerOrbInteractionBehaviour _oib = other.GetComponent<PlayerOrbInteractionBehaviour>();
            if (_oib)
            {
                if (!_oib.caughtOrb)
                {
                    _oib.CatchOrb(Entity as OrbController);
                    OnPlayerHit.Invoke();
                }
            }

            DamageReceiverBehaviour _drb = other.GetComponent<DamageReceiverBehaviour>();
            if (_drb)
            {
                OnDamageReceiverHit.Invoke(_drb);
                enemyHitCount++;
            }

            ManaBehaviour _mb = other.GetComponent<ManaBehaviour>();
            if (_mb)
            {
                CatchMana(_mb.GetMana());
            }
        }

    }
}