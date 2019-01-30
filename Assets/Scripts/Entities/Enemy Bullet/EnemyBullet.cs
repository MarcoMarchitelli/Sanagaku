using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Deirin.StateMachine;

namespace Sangaku
{
    [RequireComponent(typeof(Animator))]
    public class EnemyBullet : StateMachineBase, IEntity
    {

        #region SM

        protected override void ContextSetup()
        {
            context = new EnemyBulletSMContext(this);
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

    }

    public class EnemyBulletSMContext : IContext
    {
        public IEntity EnemyBulletEntity;

        public EnemyBulletSMContext(IEntity _enemyBulletEntity)
        {
            EnemyBulletEntity = _enemyBulletEntity;
        }
    }
}