using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Deirin.StateMachine;

namespace Sangaku
{
    [RequireComponent(typeof(Animator))]
    public class PlayerController : StateMachineBase, IEntity, IContext
    {

        #region SM

        protected override void ContextSetup()
        {
            context = new PlayerControllerSMContext(this);
        }

        protected override void OnStateChange(IState _endedState)
        {
            GoToNext();
        }      

        public void GoToNext()
        {
            StateMachine.SetTrigger("GoToNext");
        }

        #endregion

        #region Entity

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

        #endregion

        private void Start()
        {
            SetUpSM();
        }

    }

    public class PlayerControllerSMContext : IContext
    {
        public IEntity PlayerControllerEntity;

        public PlayerControllerSMContext(IEntity _playerControllerEntity)
        {
            PlayerControllerEntity = _playerControllerEntity;
        }
    }
}