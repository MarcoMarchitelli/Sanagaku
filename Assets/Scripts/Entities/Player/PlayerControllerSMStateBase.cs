using Deirin.StateMachine;

namespace Sangaku
{
    public abstract class PlayerControllerSMStateBase : StateBase
    {
        protected PlayerControllerSMContext context;

        public override void SetUp(IContext _context)
        {
            context = _context as PlayerControllerSMContext;
        }
    } 
}