
namespace Sangaku
{
    public class OrbSMFreeState : OrbSMStateBase
    {
        public override void Enter()
        {
            context.MovementBehaviour.ResetMovement();
            context.OrbBounceBehaviour.Enable(true); 
        }
    } 
}