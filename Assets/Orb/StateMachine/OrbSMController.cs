using UnityEngine;
using Deirin.StateMachine;

namespace Sangaku
{
    public class OrbSMController : StateMachineBase
    {
        [Header("Context Data")]
        public BaseEntity OrbEntity;

        #region SMBase Methods
        protected override void ContextSetup()
        {
            context = new OrbSMContext(OrbEntity, null);
        }

        protected override void OnStateChange(IState _endedState)
        {

        }
        #endregion

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

            if (!StateMachine.GetCurrentAnimatorStateInfo(0).IsName("Caught"))
                StateMachine.SetTrigger("GoToCaughtState");
        }

        public void GoToFreeState()
        {
            if (!StateMachine.GetCurrentAnimatorStateInfo(0).IsName("Free"))
                StateMachine.SetTrigger("GoToFreeState");
        }
    }

    public class OrbSMContext : IContext
    {
        public BaseEntity OrbEntity;
        public Transform CatchPoint;
        public OrbMovementBehaviour MovementBehaviour;
        public OrbBounceBehaviour OrbBounceBehaviour;
        public OrbAttractionBehaviour OrbAttractionBehaviour;
        public ManaBehaviour OrbManaBehaviour;

        public OrbSMContext(IEntity _orbEntity, Transform _catchPoint)
        {
            OrbEntity = _orbEntity as BaseEntity;
            CatchPoint = _catchPoint;
            MovementBehaviour = OrbEntity.GetComponentInChildren<OrbMovementBehaviour>();
            OrbBounceBehaviour = OrbEntity.GetComponentInChildren<OrbBounceBehaviour>();
            OrbAttractionBehaviour = OrbEntity.GetComponentInChildren<OrbAttractionBehaviour>();
            OrbManaBehaviour = OrbEntity.GetComponentInChildren<ManaBehaviour>();
        }
    }
}