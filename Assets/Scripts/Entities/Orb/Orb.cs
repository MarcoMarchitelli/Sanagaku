using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Deirin.StateMachine;

namespace Sangaku
{
    public class Orb : StateMachineBase, IEntity, IContext
{
        public List<IBehaviour> Behaviours
        {
            get;
            private set;
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

        protected override void OnStateChange(IState _endedState)
        {
            GoToNext();
        }

        public override void SetUp()
        {
            base.SetUp();
            context = this;
            Behaviours = GetComponentsInChildren<IBehaviour>().ToList();
            foreach (IBehaviour behaviour in Behaviours)
            {
                behaviour.Setup(this);
            }
        }

        public void GoToNext()
        {
            StateMachine.SetTrigger("GoToNext");
        }

        private void Start()
        {
            SetUp();
        }
    } 
}
