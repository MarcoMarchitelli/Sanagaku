using Deirin.StateMachine;

namespace Sangaku
{
    public abstract class EnemyBulletSMStateBase : StateBase
    {
        protected EnemyBulletSMContext context;

        public override void SetUp(IContext _context)
        {
            context = _context as EnemyBulletSMContext;
        }
    }
}