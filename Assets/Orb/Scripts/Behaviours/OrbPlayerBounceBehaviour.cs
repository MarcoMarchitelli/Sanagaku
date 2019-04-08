using UnityEngine.Events;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisce il rimablzo dell'orb
    /// </summary>
    public class OrbPlayerBounceBehaviour : OrbBounceBehaviour
    {
        #region Events
        /// <summary>OrbBounceBehaviour
        /// Evento lanciato alla collisione con il player
        /// </summary>
        [SerializeField] UnityEvent OnPlayerHit;
        #endregion

        /// <summary>
        /// Quantità di mana raccolta per nemico colpito
        /// </summary>
        [SerializeField] float manaModifier = 2f;

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
        protected override void Bounce(Vector3 _direction, Vector3 _normal)
        {
            bounces++;
            base.Bounce(_direction, _normal);
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

        protected override void OnTriggerEnter(Collider other)
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