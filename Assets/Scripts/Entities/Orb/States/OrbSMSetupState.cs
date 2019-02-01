
namespace Sangaku
{
    public class OrbSMSetupState : OrbSMStateBase
    {
        public override void Enter()
        {
            context.OrbEntity.SetUpEntity();
        }
    }

}