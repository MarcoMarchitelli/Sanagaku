using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Interfaccia che definisce il comportamento di un'entità
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Lista di Behaviour dell'entità
        /// </summary>
        List<IBehaviour> Behaviours { get; }
        /// <summary>
        /// Riferimento al proprio game object
        /// </summary>
        GameObject gameObject { get; }
        /// <summary>
        /// Riferimento al dato dell'entità
        /// </summary>
        IEntityData Data { get; }

        /// <summary>
        /// Funzione di setup dell'entità
        /// </summary>
        void SetUpEntity();

        /// <summary>
        /// Entity's custom update.
        /// </summary>
        void OnUpdate();

        /// <summary>
        /// Entity's custom fixed update.
        /// </summary>
        void OnFixedUpdate();

        /// <summary>
        /// Entity's custom late update.
        /// </summary>
        void OnLateUpdate();
    }

    /// <summary>
    /// Data needed for the entity to work, that comes from outside the entity's behaviours or the entity itself.
    /// </summary>
    public interface IEntityData { }
}