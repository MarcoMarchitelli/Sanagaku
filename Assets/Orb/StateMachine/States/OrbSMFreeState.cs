
namespace Sangaku
{
    /// <summary>
    /// Stato che definisce la fase di moviemnto libero dell'orb
    /// </summary>
    public class OrbSMFreeState : OrbSMStateBase
    {
        public override void Enter()
        {
            context.movementBehaviour.ResetMovement();
            context.orbBounceBehaviour.Enable(true); 
        }
    } 
}