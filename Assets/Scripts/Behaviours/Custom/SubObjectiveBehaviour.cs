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
        /// Riferimento al proprio controller
        /// </summary>
        [SerializeField] ObjectiveController objectiveController;

        /// <summary>
        /// Setup custom del Behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            if (IsSetupped && objectiveController != null)
                objectiveController.SetupSubObjective(this);
        }

        /// <summary>
        /// Funzione che notifica il proprio completamento come parte di un obbiettivo
        /// </summary>
        public void NotifyCompletion()
        {
            if (IsSetupped && objectiveController != null)
                objectiveController.SetSubObjectiveComplete(this);
        }
    }
}
