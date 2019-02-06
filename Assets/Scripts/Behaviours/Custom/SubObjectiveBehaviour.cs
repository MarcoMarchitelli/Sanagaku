using UnityEngine;
using UnityEngine.Events;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che identitifca un'entità come un sotto obbiettivo.
    /// </summary>
    public class SubObjectiveBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Evento che viene lanciato al completamento di questo sotto obbiettivo
        /// </summary>
        [SerializeField] UnityEvent<SubObjectiveBehaviour> OnSubObjectiveCompletion;
        /// <summary>
        /// Evento lanciato al setup del behaviour
        /// </summary>
        [SerializeField] UnityEvent<SubObjectiveBehaviour> OnSubObjectiveSetup;

        /// <summary>
        /// Setup custom del Behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            if (IsSetupped)
                OnSubObjectiveSetup.Invoke(this);
        }

        /// <summary>
        /// Funzione che notifica il proprio completamento come parte di un obbiettivo
        /// </summary>
        public void NotifyCompletion()
        {
            if (IsSetupped)
                OnSubObjectiveCompletion.Invoke(this);
        }
    }
}
