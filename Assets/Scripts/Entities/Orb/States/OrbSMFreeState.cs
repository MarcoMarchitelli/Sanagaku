using UnityEngine;

namespace Sangaku
{
    public class OrbSMFreeState : OrbSMStateBase
    {
        public override void Enter()
        {
            context.OrbEntity.ToggleBehaviors(true);
            context.movementBehaviour.Setup(context.OrbEntity);
        }
    } 
}