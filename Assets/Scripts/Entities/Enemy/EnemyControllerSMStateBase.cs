using Deirin.StateMachine;

namespace Sangaku
{
    public class EnemyControllerSMStateBase : StateBase
    {

        protected EnemyControllerSMContext context;

        public override void SetUp(IContext _context)
        {
            context = _context as EnemyControllerSMContext;
        }

    } 
}