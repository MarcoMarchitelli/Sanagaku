using UnityEngine;

namespace Sangaku
{
    public class OrbSMCaughtState : OrbSMStateBase
    {
        Transform orbTransform;

        public override void Enter()
        {
            foreach (BaseBehaviour baseBehaviour in context.OrbEntity.Behaviours)
            {
                baseBehaviour.Enable(false);
            }
            orbTransform = context.OrbEntity.transform;
            context.orbManaBehaviour.ResetMana();
        }

        public override void Tick()
        {
            orbTransform.position = context.CatchPoint.position;
            orbTransform.rotation = context.CatchPoint.rotation;
        }

        public override void Exit()
        {
            foreach (BaseBehaviour baseBehaviour in context.OrbEntity.Behaviours)
            {
                baseBehaviour.enabled = true;
            }
            orbTransform = null;
        }
    }

}