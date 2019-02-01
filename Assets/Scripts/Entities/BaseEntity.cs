using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Sangaku
{
    public abstract class BaseEntity : MonoBehaviour, IEntity
    {
        public List<IBehaviour> Behaviours
        {
            get;
            private set;
        }

        public void SetUpEntity()
        {
            Behaviours = GetComponentsInChildren<IBehaviour>().ToList();
            foreach (IBehaviour behaviour in Behaviours)
            {
                behaviour.Setup(this);
            }
        }
    } 
}