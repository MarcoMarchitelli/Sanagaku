using UnityEngine;

namespace Sangaku
{
    public class OrbSMCaughtState : OrbSMStateBase
    {
        Transform orbTransform;

        public override void Enter()
        {
            context.OrbEntity.ToggleBehaviors(false);
            orbTransform = context.OrbEntity.transform;
        }

        public override void Tick()
        {
            orbTransform.position = context.CatchPoint.position;
            orbTransform.rotation = context.CatchPoint.rotation;
        }

        public override void Exit()
        {
            context.OrbEntity.ToggleBehaviors(true);
            orbTransform = null;
        }
    }

}