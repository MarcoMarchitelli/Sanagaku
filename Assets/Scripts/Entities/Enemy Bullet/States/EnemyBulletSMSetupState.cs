
namespace Sangaku
{
    public class EnemyBulletSMSetupState : EnemyBulletSMStateBase
    {
        public override void Enter()
        {
            context.EnemyBulletEntity.SetUpEntity();
        }
    }

}