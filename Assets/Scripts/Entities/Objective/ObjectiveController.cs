using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Sangaku
{
    /// <summary>
    /// Entita che identifica un obbiettivo. 
    /// </summary>
    public class ObjectiveController : BaseEntity
    {
        /// <summary>
        /// Evento lanciato quando l'obbiettvo è completato
        /// </summary>
        [SerializeField] UnityEvent OnObjectiveCompletion;

        /// <summary>
        /// Lista di sotto obbiettivi che formano questo obbiettivo
        /// </summary>
        List<SubObjectiveCompletition> objectives;

        //HACK: andrà rimosso quando ci sarà un state machine del livello
        // per ora va lasciato se no il componente non si setuppa
        private void Start()
        {
            base.SetUpEntity();
        }
        //--------------------------------------------

        #region API
        /// <summary>
        /// Setup custom del Behaviour
        /// </summary>
        public override void CustomSetup()
        {
            objectives = new List<SubObjectiveCompletition>();
        }

        /// <summary>
        /// Funzione che setta un sotto obbiettivo come completo
        /// </summary>
        /// <param name="_subObjective"></param>
        public void SetSubObjectiveComplete(SubObjectiveBehaviour _subObjective)
        {
            if (objectives == null)
                return;

            // setto completato il sotto obbiettivo
            for (int i = 0; i < objectives.Count; i++)
            {
                if (objectives[i].SubObjective == _subObjective)
                {
                    objectives[i].IsCompleted = true;
                    break;
                }
            }

            // se sono tutti completati l'obbiettivo è completato
            if (objectives.Where(o => o.IsCompleted == false).Count() == 0)
                OnObjectiveCompletion.Invoke();

            objectives.Clear();
        }

        /// <summary>
        /// Funzione che aggiunge un sotto obbiettivo alla lista di quelli controllati
        /// </summary>
        /// <param name="_subObjective"></param>
        public void SetupSubObjective(SubObjectiveBehaviour _subObjective)
        {
            if (objectives == null)
                return;

            objectives.Add(new SubObjectiveCompletition(_subObjective));
        } 
        #endregion

        /// <summary>
        /// Classe che gestisce lo stato di completamento di un sotto obbiettivo
        /// </summary>
        class SubObjectiveCompletition
        {
            /// <summary>
            /// Riferimento al sotto obbiettivo
            /// </summary>
            public SubObjectiveBehaviour SubObjective { get; private set; }
            /// <summary>
            /// Stato di completamento
            /// </summary>
            public bool IsCompleted { get; set; }

            /// <summary>
            /// Costruttore
            /// </summary>
            /// <param name="_objective"></param>
            public SubObjectiveCompletition(SubObjectiveBehaviour _objective)
            {
                SubObjective = _objective;
                IsCompleted = false;
            }
        }
    }
}
