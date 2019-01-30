
namespace Sangaku
{
    public class EnemyControllerSMSetupState : EnemyControllerSMStateBase
    {
        public override void Enter()
        {
            context.EnemyEntity.SetUpEntity();
        }
    } 
}