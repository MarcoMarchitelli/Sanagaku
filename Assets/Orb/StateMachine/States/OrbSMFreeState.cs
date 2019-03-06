using UnityEngine;

namespace Sangaku
{
    public class OrbSMFreeState : OrbSMStateBase
    {
        public override void Enter()
        {
            context.movementBehaviour.ResetMovement();
            context.orbBounceBehaviour.Enable(true); 
        }
    } 
}