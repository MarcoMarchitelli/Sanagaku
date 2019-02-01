using UnityEngine;
using Deirin.StateMachine;

namespace Sangaku
{
    public class OrbSMController : StateMachineBase
    {
        [Header("Context Data")]
        public BaseEntity OrbEntity;
        [HideInInspector]
        public Transform CatchPoint;

        protected override void ContextSetup()
        {
            context = new OrbSMContext(OrbEntity, CatchPoint);
        }

        protected override void OnStateChange(IState _endedState)
        {
        }

        public void GoToCaughtState(Transform _catchPoint)
        {
            ResetOrbContext(new OrbSMContext(OrbEntity, _catchPoint));
            StateMachine.SetTrigger("GoToCaughtState");
        }

        public void GoToFreeState()
        {
            StateMachine.SetTrigger("GoToFreeState");
        }

        void ResetOrbContext(OrbSMContext _newContext)
        {
            foreach (OrbSMStateBase state in States)
            {
                state.SetUp(_newContext);
            }
        }

    }

    public class OrbSMContext : IContext
    {
        public BaseEntity OrbEntity;
        public OrbMovementBehaviour movementBehaviour;
        public Transform CatchPoint;

        public OrbSMContext(IEntity _orbEntity, Transform _catchPoint)
        {
            OrbEntity = _orbEntity as BaseEntity;
            CatchPoint = _catchPoint;
            movementBehaviour = OrbEntity.GetComponentInChildren<OrbMovementBehaviour>();
        }
    }

}