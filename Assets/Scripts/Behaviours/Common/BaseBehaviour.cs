using UnityEngine;

namespace Sangaku
{
    public abstract class BaseBehaviour : MonoBehaviour, IBehaviour
    {
        /// <summary>
        /// Riferimento all'entitià che controlla il Behaviour
        /// </summary>
        public IEntity Entity { get; private set; }
        /// <summary>
        /// True se il Behaviour è stato setuppato, false altrimenti
        /// </summary>
        public bool IsSetupped { get; private set; }

        /// <summary>
        /// Base obligatory setup for every Behaviour.
        /// </summary>
        /// <param name="_entity"></param>
        public void Setup(IEntity _entity)
        {
            Entity = _entity;
            IsSetupped = true;
            CustomSetup();
        }

        /// <summary>
        /// Optional setup unique to every Behaviour that implements it.
        /// </summary>
        protected virtual void CustomSetup()
        {

        }

    }
}