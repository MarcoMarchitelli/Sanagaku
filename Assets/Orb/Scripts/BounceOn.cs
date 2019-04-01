using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Tells the Orb that hits this object how to behave. Requires a collider.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class BounceOn : MonoBehaviour
    {
        /// <summary>
        /// Tipo di bounce di questo component
        /// </summary>
        [SerializeField] BounceType behaviourType;
        /// <summary>
        /// Tipo di bounce di questo component
        /// </summary>
        public BounceType BehaviourType { get { return behaviourType; } set { behaviourType = value; } }

        /// <summary>
        /// Tipi di bounce di questo component
        /// </summary>
        public enum BounceType
        {
            Ignore,
            Realistic,
            Destroy
        }
    }

}