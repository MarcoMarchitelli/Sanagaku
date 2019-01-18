using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Sangaku
{
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour, IEntity
    {
        public List<IBehaviour> Behaviours
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        private Animator stateMachine;
        public Animator StateMachine
        {
            get
            {
                if (!stateMachine)
                    stateMachine = GetComponent<Animator>();
                return stateMachine;
            }
        }

    }
}