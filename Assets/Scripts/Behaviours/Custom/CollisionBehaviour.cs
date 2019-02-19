using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che permette di interfacciarsi con le collisoni di unity in modo generico
    /// </summary>
    public class CollisionBehaviour : BaseBehaviour
    {
        #region Trigger
        [Header("Trigger Events", order = 1)]
        [SerializeField] UnityVoidEvent OnTriggerEnterEvent;
        [SerializeField] UnityVoidEvent OnTriggerStayEvent;
        [SerializeField] UnityVoidEvent OnTriggerExitEvent;
        [Header("Trigger Parameters", order = 2)]
        [SerializeField] List<MonoBehaviour> entitiesToIgnoreForTrigger;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!CheckEntitiesToIgnoreForTrigger(entitiesToIgnoreForTrigger, other))
            {
                OnTriggerEnterEvent.Invoke();
                TriggerEnter(); 
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!CheckEntitiesToIgnoreForTrigger(entitiesToIgnoreForTrigger, other))
            {
                OnTriggerStayEvent.Invoke();
                TriggerStay(); 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!CheckEntitiesToIgnoreForTrigger(entitiesToIgnoreForTrigger, other))
            {
                OnTriggerExitEvent.Invoke();
                TriggerExit();
            }
        } 

        protected virtual void TriggerEnter() { }
        protected virtual void TriggerStay() { }
        protected virtual void TriggerExit() { }

        protected bool CheckEntitiesToIgnoreForTrigger(List<MonoBehaviour> _entitiesToIgnore, Collider _collider)
        {
            for (int i = 0; i < _entitiesToIgnore.Count; i++)
            {
                if (_collider.GetComponent(_entitiesToIgnore[i].GetType()))
                    return true;
            }
            return false;
        }
        #endregion

        #region Collision
        [Header("Collision Events", order = 3)]
        [SerializeField] UnityVoidEvent OnCollisionEnterEvent;
        [SerializeField] UnityVoidEvent OnCollisionStayEvent;
        [SerializeField] UnityVoidEvent OnCollisionExitEvent;
        [Header("Collision Parameters", order = 4)]
        [SerializeField] List<MonoBehaviour> entitiesToIgnoreForCollision;

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (!CheckEntitiesToIgnoreForCollider(entitiesToIgnoreForCollision, collision))
            {
                OnCollisionEnterEvent.Invoke();
                CollisionEnter(); 
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!CheckEntitiesToIgnoreForCollider(entitiesToIgnoreForCollision, collision))
            {
                OnCollisionStayEvent.Invoke();
                CollisionStay(); 
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!CheckEntitiesToIgnoreForCollider(entitiesToIgnoreForCollision, collision))
            {
                OnCollisionExitEvent.Invoke();
                CollisionExit(); 
            }
        }

        protected virtual void CollisionEnter() { }
        protected virtual void CollisionStay() { }
        protected virtual void CollisionExit() { }

        protected bool CheckEntitiesToIgnoreForCollider(List<MonoBehaviour> _entitiesToIgnore, Collision _collision)
        {
            for (int i = 0; i < _entitiesToIgnore.Count; i++)
            {
                if (_collision.collider.GetComponent(_entitiesToIgnore[i].GetType()))
                    return true;
            }
            return false;
        }
        #endregion
    } 
}
