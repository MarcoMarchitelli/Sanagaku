using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    [RequireComponent(typeof(Collider))]
    public class DamageDealerBehaviour : MonoBehaviour, IBehaviour
    {
        #region Events
        [SerializeField] UnityFloatEvent OnDamageDealt; 
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

        /// <summary>
        /// The amount of damage to inflict
        /// </summary>
        [SerializeField] float damage;

        /// <summary>
        /// Funzione che infligge danno al receiver rilevato
        /// </summary>
        /// <param name="_receiver"></param>
        public void DealDamage(DamageReceiverBehaviour _receiver)
        {
            _receiver.SetHealth(-damage);
            OnDamageDealt.Invoke(damage);
        }

        private void OnTriggerEnter(Collider other)
        {
            DamageReceiverBehaviour receiver = other.GetComponent<DamageReceiverBehaviour>();
            if (receiver)
            {
                DealDamage(receiver);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            DamageReceiverBehaviour receiver = collision.collider.GetComponent<DamageReceiverBehaviour>();
            if (receiver)
            {
                DealDamage(receiver);
            }
        }

    }

}