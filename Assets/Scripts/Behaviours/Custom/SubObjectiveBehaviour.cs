using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che identitifca un'entità come un sotto obbiettivo.
    /// </summary>
    [System.Serializable]
    public class SubObjectiveBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Evento lanciato al setup del behaviour
        /// </summary>
        [SerializeField] UnitySubObjectiveEvent OnSubObjectiveSetup;
        /// <summary>
        /// Evento che viene lanciato al completamento di questo sotto obbiettivo
        /// </summary>
        [SerializeField] UnitySubObjectiveEvent OnSubObjectiveCompletion;

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
