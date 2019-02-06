using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Sangaku
{
    public class TestEntity : MonoBehaviour, IEntity
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

        private void Start()
        {
            SetUpEntity();
        }

    } 
}
