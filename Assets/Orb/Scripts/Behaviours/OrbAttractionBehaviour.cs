using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Componente che definisce il comportamente di attrazione verso un punto dell'orb
    /// </summary>
    [RequireComponent(typeof(OrbMovementBehaviour))]
    public class OrbAttractionBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Evento lanciato all'inizio dell'attrazione
        /// </summary>
        [SerializeField] UnityVoidEvent OnAttractionStart;
        /// <summary>
        /// Evento lanciato alla fine dell'attrazione
        /// </summary>
        [SerializeField] UnityVoidEvent OnAttractionEnd;

        /// <summary>
        /// Velocità con cui l'orb si muove verso il player
        /// </summary>
        [SerializeField] float velocity;

        /// <summary>
        /// Riferimento al PlayerAttractionBehaviour
        /// </summary>
        PlayerAttractionBehaviour playerAttractionBehaviour;
        /// <summary>
        /// Riferimento al OrbMovementBehaviour
        /// </summary>
        OrbMovementBehaviour movementBehaviour;

        /// <summary>
        /// Custom setup del behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            movementBehaviour = Entity.GetBehaviour<OrbMovementBehaviour>();
        }

        /// <summary>
        /// Funzione che muove l'orb verso la posizione passata come parametro
        /// </summary>
        /// <param name="_position"></param>
        public void MoveTowardsPosition(Vector3 _position)
        {
            if (IsSetupped)
                transform.position = Vector3.MoveTowards(transform.position, _position, velocity * Time.deltaTime);
        }

        /// <summary>
        /// Funzion che setta il riferimento al PlayerAttractionBehaviour
        /// </summary>
        /// <param name="_attractionBehaviour"></param>
        public void SetPlayerAttractionBehaviour(PlayerAttractionBehaviour _attractionBehaviour)
        {
            if (!IsSetupped)
                return;

            playerAttractionBehaviour = _attractionBehaviour;

            if (playerAttractionBehaviour != null)
                OnAttractionStart.Invoke();
            else
                OnAttractionEnd.Invoke();
        }

        public override void Enable(bool _value)
        {
            base.Enable(_value);

            if (!_value && playerAttractionBehaviour)
            {
                playerAttractionBehaviour.RemoveOrbFromAttraction(this);
                playerAttractionBehaviour = null;
            }
        }
    }
}
