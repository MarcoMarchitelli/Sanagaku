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

        #region SMBase Methods
        protected override void ContextSetup()
        {
            context = new OrbSMContext(OrbEntity, CatchPoint);
        }

        protected override void OnStateChange(IState _endedState)
        {

        }

        public void GoToCaughtState()
        {
            StateMachine.SetTrigger("GoToCaughtState");
        }

        public void GoToFreeState()
        {
            StateMachine.SetTrigger("GoToFreeState");
        }
        #endregion

        public void OrbSMSetUp(Transform _catchPoint)
        {
            CatchPoint = _catchPoint;
            SetUpSM();
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