using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che permette di interfacciarsi con i trigger di unity in modo generico, gestendo una lista escusiva di oggetti da controllare
    /// </summary>
    public class TriggerExclusiveBehaviour : BaseBehaviour
    {
        [Header("Trigger Events", order = 1)]
        [SerializeField] UnityColliderEvent OnTriggerEnterEvent;
        [SerializeField] UnityColliderEvent OnTriggerStayEvent;
        [SerializeField] UnityColliderEvent OnTriggerExitEvent;
        [Header("Trigger Parameters", order = 2)]
        [SerializeField] List<MonoBehaviour> entitiesToIgnoreForTrigger;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!CheckEntitiesToIgnoreForTrigger(entitiesToIgnoreForTrigger, other))
            {
                OnTriggerEnterEvent.Invoke(other);
                TriggerEnter(other); 
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!CheckEntitiesToIgnoreForTrigger(entitiesToIgnoreForTrigger, other))
            {
                OnTriggerStayEvent.Invoke(other);
                TriggerStay(other); 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!CheckEntitiesToIgnoreForTrigger(entitiesToIgnoreForTrigger, other))
            {
                OnTriggerExitEvent.Invoke(other);
                TriggerExit(other);
            }
        } 

        protected virtual void TriggerEnter(Collider _other) { }
        protected virtual void TriggerStay(Collider _other) { }
        protected virtual void TriggerExit(Collider _other) { }

        /// <summary>
        /// Funzione che controlla se ho colliso con un'entità da ignorare
        /// </summary>
        /// <param name="_entitiesToIgnore"></param>
        /// <param name="_collider"></param>
        /// <returns></returns>
        protected bool CheckEntitiesToIgnoreForTrigger(List<MonoBehaviour> _entitiesToIgnore, Collider _collider)
        {
            if (_entitiesToIgnore == null)
                return false;

            for (int i = 0; i < _entitiesToIgnore.Count; i++)
            {
                if (_entitiesToIgnore[i] != null && _collider.GetComponent(_entitiesToIgnore[i].GetType()))
                    return true;
            }
            return false;
        }
    } 
}
