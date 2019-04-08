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
        [SerializeField] UnityColliderEvent OnTriggerEnterEvent;
        [SerializeField] UnityColliderEvent OnTriggerStayEvent;
        [SerializeField] UnityColliderEvent OnTriggerExitEvent;

        protected virtual void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent.Invoke(other);
            TriggerEnter(other);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerStayEvent.Invoke(other);
            TriggerStay(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitEvent.Invoke(other);
            TriggerExit(other);
        }

        protected virtual void TriggerEnter(Collider _other) { }
        protected virtual void TriggerStay(Collider _other) { }
        protected virtual void TriggerExit(Collider _other) { }
        #endregion

        #region Collision
        [Header("Collision Events", order = 2)]
        [SerializeField] UnityCollisionEvent OnCollisionEnterEvent;
        [SerializeField] UnityCollisionEvent OnCollisionStayEvent;
        [SerializeField] UnityCollisionEvent OnCollisionExitEvent;

        protected virtual void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterEvent.Invoke(collision);
            CollisionEnter(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            OnCollisionStayEvent.Invoke(collision);
            CollisionStay(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnCollisionExitEvent.Invoke(collision);
            CollisionExit(collision);
        }

        protected virtual void CollisionEnter(Collision _collision) { }
        protected virtual void CollisionStay(Collision _collision) { }
        protected virtual void CollisionExit(Collision _collision) { }
        #endregion
    }
}
