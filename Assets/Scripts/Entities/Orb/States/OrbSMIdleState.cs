
namespace Sangaku
{
    public class OrbSMIdleState : OrbSMStateBase
    {
        public override void Enter()
        {
            context.OrbEntity.ToggleBehaviors(false);
        }
    }
}