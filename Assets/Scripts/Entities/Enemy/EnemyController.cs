using System.Collections.Generic;
using Deirin.StateMachine;
using System.Linq;
using UnityEngine;

namespace Sangaku
{
    [RequireComponent(typeof(Animator))]
    public class EnemyController : StateMachineBase, IEntity
    {
        #region SM

        protected override void ContextSetup()
        {
            context = new EnemyControllerSMContext(this);
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

    public class EnemyControllerSMContext : IContext
    {
        public IEntity EnemyEntity;

        public EnemyControllerSMContext(IEntity _enemyEntity)
        {
            EnemyEntity = _enemyEntity;
        }
    }

}