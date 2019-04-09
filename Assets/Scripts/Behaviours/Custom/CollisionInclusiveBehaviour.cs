using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che permette di interfacciarsi con le collisoni di unity in modo generico, gestendo una lista inclusiva di oggetti da controllare
    /// </summary>
    public class CollisionInclusiveBehaviour : BaseBehaviour
    {
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
    } 
}
