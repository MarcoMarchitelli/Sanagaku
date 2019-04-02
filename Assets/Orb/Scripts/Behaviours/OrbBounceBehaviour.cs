using UnityEngine.Events;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisce il rimablzo dell'orb
    /// </summary>
    public class OrbBounceBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Quantità di mana raccolta per nemico colpito
        /// </summary>
        [SerializeField] float manaModifier = 2f;

        #region Events
        /// <summary>
        /// Evento lanciato al bounce della pallina
        /// </summary>
        [SerializeField] UnityVector3Event OnBounce;
        /// <summary>
        /// Evento lanciato alla collisione con un DamageReceiver
        /// </summary>
        [SerializeField] UnityDamageReceiverEvent OnDamageReceiverHit;
        /// <summary>
        /// Evento lanciato alla collisione con il player
        /// </summary>
        [SerializeField] UnityEvent OnPlayerHit;
        /// <summary>
        /// Evento lanciato alla collisione con un elemento che distrugge l'orb
        /// </summary>
        [SerializeField] UnityEvent OnDestroyHit;
        #endregion

        /// <summary>
        /// Dati dell'orb
        /// </summary>
        OrbControllerData data;
        /// <summary>
        /// Numero di bounce dell'orb
        /// </summary>
        int bounces;
        /// <summary>
        /// Contatore di hit ai nemici
        /// </summary>
        int enemyHitCount;
        /// <summary>
        /// Riferimento al ManaBehaviour
        /// </summary>
        ManaBehaviour mana;

        /// <summary>
        /// Setup custom del behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            enemyHitCount = 0;
            mana = Entity.gameObject.GetComponent<ManaBehaviour>();
            data = Entity.Data as OrbControllerData;
            isDepartingFromPlayer = true;
            oldDistanceFromPlayer = Vector3.Distance(transform.position, data.PlayerReference.transform.position);
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
        void HandleBounceOnBehaviour(BounceOn _b, Vector3 _hitNormal)
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

        /// <summary>
        /// Handles the mana obtained on ManaBehaviour hit.
        /// </summary>
        /// <param name="_manaAmount"></param>
        public void CatchMana(float _manaAmount)
        {
            if (enemyHitCount <= 1)
                mana.AddMana(_manaAmount);
            else
                mana.AddMana(_manaAmount + (manaModifier * (enemyHitCount - 1)));
        }

        float distanceFromPlayer;
        float oldDistanceFromPlayer;
        bool isDepartingFromPlayer = true;

        void CheckPlayerDistance(Vector3 _playerPosition)
        {
            distanceFromPlayer = Vector3.Distance(transform.position, _playerPosition);
            if (distanceFromPlayer > oldDistanceFromPlayer)
                isDepartingFromPlayer = true;
            else
                isDepartingFromPlayer = false;
            oldDistanceFromPlayer = distanceFromPlayer;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsSetupped)
                return;
            BounceOn _b = collision.collider.GetComponent<BounceOn>();
            if (_b)
                HandleBounceOnBehaviour(_b, collision.contacts[0].normal);
            else
                Bounce(transform.forward, collision.contacts[0].normal);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsSetupped)
                return;
            PlayerOrbInteractionBehaviour _oib = other.GetComponent<PlayerOrbInteractionBehaviour>();
            if (_oib)
            {
                if (!_oib.caughtOrb && !isDepartingFromPlayer)
                {
                    _oib.CatchOrb(Entity as OrbController);
                    OnPlayerHit.Invoke();
                }
            }

            DamageReceiverBehaviour _drb = other.GetComponent<DamageReceiverBehaviour>();
            if (_drb && !_drb.Entity.GetType().IsAssignableFrom(typeof(PlayerController))) // controllo che non sia un player
            {
                OnDamageReceiverHit.Invoke(_drb);
            }

            ManaBehaviour _mb = other.GetComponent<ManaBehaviour>();
            if (_mb && _mb.Entity.GetType().IsAssignableFrom(typeof(EnemyController)))
            {
                enemyHitCount++;
                CatchMana(_mb.GetMana());
            }
        }

        public override void OnUpdate()
        {
            if (data.PlayerReference)
                CheckPlayerDistance(data.PlayerReference.transform.position);
        }

        public override void Enable(bool _value)
        {
            enemyHitCount = 0;
            isDepartingFromPlayer = true;
            oldDistanceFromPlayer = Vector3.Distance(transform.position, data.PlayerReference.transform.position);
            base.Enable(_value);
        }
    }
}