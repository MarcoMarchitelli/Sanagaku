using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che permette di interfacciarsi con le collisoni di unity in modo generico, gestendo una lista inclusiva di oggetti da controllare
    /// </summary>
    public class CollisionInclusiveBehaviour : BaseBehaviour
    {
        #region Trigger
        [Header("Trigger Events", order = 1)]
        [SerializeField] UnityColliderEvent OnTriggerEnterEvent;
        [SerializeField] UnityColliderEvent OnTriggerStayEvent;
        [SerializeField] UnityColliderEvent OnTriggerExitEvent;
        [Header("Trigger Parameters", order = 2)]
        [SerializeField] List<MonoBehaviour> entitiesToDetectForTrigger;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (CheckEntitiesToDetectForTrigger(entitiesToDetectForTrigger, other))
            {
                OnTriggerEnterEvent.Invoke(other);
                TriggerEnter(other); 
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (CheckEntitiesToDetectForTrigger(entitiesToDetectForTrigger, other))
            {
                OnTriggerStayEvent.Invoke(other);
                TriggerStay(other); 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (CheckEntitiesToDetectForTrigger(entitiesToDetectForTrigger, other))
            {
                OnTriggerExitEvent.Invoke(other);
                TriggerExit(other);
            }
        } 

        protected virtual void TriggerEnter(Collider _other) { }
        protected virtual void TriggerStay(Collider _other) { }
        protected virtual void TriggerExit(Collider _other) { }

        /// <summary>
        /// Funzione che controlla se ho colliso con un'entità da controllare
        /// </summary>
        /// <param name="_entitiesToDetect"></param>
        /// <param name="_collider"></param>
        /// <returns></returns>
        protected bool CheckEntitiesToDetectForTrigger(List<MonoBehaviour> _entitiesToDetect, Collider _collider)
        {
            if (_entitiesToDetect == null)
                return true;

            for (int i = 0; i < _entitiesToDetect.Count; i++)
            {
                if (_entitiesToDetect[i] != null && _collider.GetComponent(_entitiesToDetect[i].GetType()))
                    return true;
            }
            return false;
        }
        #endregion

        #region Collision
        [Header("Collision Events", order = 3)]
        [SerializeField] UnityCollisionEvent OnCollisionEnterEvent;
        [SerializeField] UnityCollisionEvent OnCollisionStayEvent;
        [SerializeField] UnityCollisionEvent OnCollisionExitEvent;
        [Header("Collision Parameters", order = 4)]
        [SerializeField] List<MonoBehaviour> entitiesToDetectForCollision;

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (CheckEntitiesToDetectForCollider(entitiesToDetectForCollision, collision))
            {
                OnCollisionEnterEvent.Invoke(collision);
                CollisionEnter(collision); 
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (CheckEntitiesToDetectForCollider(entitiesToDetectForCollision, collision))
            {
                OnCollisionStayEvent.Invoke(collision);
                CollisionStay(collision); 
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (CheckEntitiesToDetectForCollider(entitiesToDetectForCollision, collision))
            {
                OnCollisionExitEvent.Invoke(collision);
                CollisionExit(collision); 
            }
        }

        protected virtual void CollisionEnter(Collision _collision) { }
        protected virtual void CollisionStay(Collision _collision) { }
        protected virtual void CollisionExit(Collision _collision) { }

        /// <summary>
        /// Funzione che controlla se ho colliso con un'entità da controllare
        /// </summary>
        /// <param name="_entitiesToDetect"></param>
        /// <param name="_collision"></param>
        /// <returns></returns>
        protected bool CheckEntitiesToDetectForCollider(List<MonoBehaviour> _entitiesToDetect, Collision _collision)
        {
            if (_entitiesToDetect == null)
                return true;

            for (int i = 0; i < _entitiesToDetect.Count; i++)
            {
                if (_entitiesToDetect[i] != null && _collision.collider.GetComponent(_entitiesToDetect[i].GetType()))
                    return true;
            }
            return false;
        }
        #endregion
    } 
}
