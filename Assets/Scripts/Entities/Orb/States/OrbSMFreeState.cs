
namespace Sangaku
{
    public class OrbSMFreeState : OrbSMStateBase
    {
        public override void Enter()
        {
            context.movementBehaviour.Setup(context.OrbEntity);
        }
    } 
}