using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Componente che definisce il comportamente di attrazione verso un punto dell'orb
    /// </summary>
    public class OrbAttractionBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Velocità con cui l'orb si muove verso il player
        /// </summary>
        [SerializeField] float velocity;

        /// <summary>
        /// Riferimento al PlayerAttractionBehaviour
        /// </summary>
        PlayerAttractionBehaviour playerAttractionBehaviour;

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
            if (IsSetupped)
                playerAttractionBehaviour = _attractionBehaviour;
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
