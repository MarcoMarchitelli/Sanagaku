
namespace Sangaku
{
    public class OrbSMFreeState : OrbSMStateBase
    {
        public override void Enter()
        {
            context.orbMovementBehaviour.ResetMovement();
            context.orbBounceBehaviour.Enable(true); 
        }
    } 
}