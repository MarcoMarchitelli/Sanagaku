using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che permette di interfacciarsi con i trigger di unity in modo generico
    /// </summary>
    public class TriggerBehaviour : BaseBehaviour
    {
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
    }
}
