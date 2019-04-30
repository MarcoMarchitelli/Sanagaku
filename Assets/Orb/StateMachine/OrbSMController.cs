using UnityEngine;
using Deirin.StateMachine;

namespace Sangaku
{
    public class OrbSMController : StateMachineBase
    {
        [Header("Context Data")]
        public BaseEntity OrbEntity;

        protected override void ContextSetup()
        {
            context = new OrbSMContext(OrbEntity, null);
        }

        protected override void OnStateChange(IState _endedState) { }

        public void GoToCaughtState(Transform _catchPoint)
        {
            foreach (OrbSMStateBase state in States)
            {
                if(state.ID == "Caught")
                {
                    state.context.CatchPoint = _catchPoint;
                    break;
                }
            }
            StateMachine.SetTrigger("GoToCaughtState");
        }

        public void GoToFreeState()
        {
            StateMachine.SetTrigger("GoToFreeState");
        }
    }

    public class OrbSMContext : IContext
    {
        public BaseEntity OrbEntity;
        public OrbMovementBehaviour orbMovementBehaviour;
        public OrbBounceBehaviour orbBounceBehaviour;
        public Transform CatchPoint;
        public ManaBehaviour orbManaBehaviour;

        public OrbSMContext(IEntity _orbEntity, Transform _catchPoint)
        {
            OrbEntity = _orbEntity as BaseEntity;
            CatchPoint = _catchPoint;
            orbMovementBehaviour = OrbEntity.GetComponentInChildren<OrbMovementBehaviour>();
            orbBounceBehaviour = OrbEntity.GetComponentInChildren<OrbBounceBehaviour>();
            orbManaBehaviour = OrbEntity.GetComponentInChildren<ManaBehaviour>();
        }
    }
}