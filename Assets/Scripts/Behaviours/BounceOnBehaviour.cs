using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Tells the bullet that hits this object how to behave. Requires a collider.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class BounceOnBehaviour : MonoBehaviour, IBehaviour
    {
        public enum Type { realistic = 1, catchAndFire = 2, goThrough = 3, destroy = 4 }
        [Tooltip("1 = realistic, 2 = CNF, 3 = goThrough, 4 = destroy")] public Type BehaviourType;

        /// <summary>
        /// Riferimento all'entitià che controlla il Behaviour
        /// </summary>
        public IEntity Entity { get; private set; }
        /// <summary>
        /// True se il Behaviour è stato setuppato, false altrimenti
        /// </summary>
        public bool IsSetupped { get; private set; }

        /// <summary>
        /// Eseguo il setup del behaviour
        /// </summary>
        /// <param name="_entity"></param>
        public void Setup(IEntity _entity)
        {
            Entity = _entity;
            IsSetupped = true;
        }

    }

}