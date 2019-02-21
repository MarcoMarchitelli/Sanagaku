using UnityEngine;
using UnityEngine.Events;

namespace Sangaku
{
    /// <summary>
    /// Entità che identifica il nemico
    /// </summary>
    public class EnemyController : BaseEntity
    {
        /// <summary>
        /// Evento lanciato al setup dell'entità
        /// </summary>
        [SerializeField] UnityEvent OnEntitySetup;

        /// <summary>
        /// Setup custom del behaviour
        /// </summary>
        public override void CustomSetup()
        {
            OnEntitySetup.Invoke();
        }
    }
}