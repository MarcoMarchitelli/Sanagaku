using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Sangaku
{
    /// <summary>
    /// Classe astratta che definisce un'entità base
    /// </summary>
    public abstract class BaseEntity : MonoBehaviour, IEntity
    {
        /// <summary>
        /// Riferimento al dato generico dell'entità
        /// </summary>
        public IEntityData Data { get; set; }
        /// <summary>
        /// List of IBehaviours that describe this Entity.
        /// </summary>
        public List<IBehaviour> Behaviours { get; private set; }
        /// <summary>
        /// True se il Behaviour è stato setuppato, false altrimenti
        /// </summary>
        public bool IsSetupped { get; private set; }

        /// <summary>
        /// Basic Entity setup. Every Entity needs to implement this to function.
        /// </summary>
        public void SetUpEntity()
        {
            Behaviours = GetComponentsInChildren<IBehaviour>().ToList();
            CustomSetup();
            foreach (IBehaviour behaviour in Behaviours)
            {
                behaviour.Setup(this);
            }

            IsSetupped = true;
        }

        /// <summary>
        /// Additional Entity setup. Unique to every Entity that implements it.
        /// </summary>
        public virtual void CustomSetup() { }

        /// <summary>
        /// Toggles all behaviours on or off.
        /// </summary>
        /// <param name="_value"></param>
        public void Enable(bool _value)
        {
            foreach (IBehaviour behaviour in Behaviours)
            {
                behaviour.Enable(_value);
            }
        }

        /// <summary>
        /// Funzione che ritorna il behaviour corrispondente al tipo passato
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetBehaviour<T>() where T : IBehaviour
        {
            foreach (IBehaviour b in Behaviours)
            {
                if (b.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)b;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Behaviour's custom late update.
        /// </summary>
        public virtual void OnUpdate()
        {
            if (!IsSetupped)
                return;

            for (int i = 0; i < Behaviours.Count; i++)
            {
                Behaviours[i].OnUpdate();
            }
        }

        /// <summary>
        /// Behaviour's custom update.
        /// </summary>
        public virtual void OnFixedUpdate()
        {
            if (!IsSetupped)
                return;

            for (int i = 0; i < Behaviours.Count; i++)
            {
                Behaviours[i].OnFixedUpdate();
            }
        }

        /// <summary>
        /// Behaviour's custom fixed update.
        /// </summary>
        public virtual void OnLateUpdate()
        {
            if (!IsSetupped)
                return;

            for (int i = 0; i < Behaviours.Count; i++)
            {
                Behaviours[i].OnLateUpdate();
            }
        }

        protected void Update()
        {
            OnUpdate();
        }

        protected void FixedUpdate()
        {
            OnFixedUpdate();
        }

        protected void LateUpdate()
        {
            OnLateUpdate();
        }
    } 
}