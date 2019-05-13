using UnityEngine;

namespace Sangaku
{
    public class OrbSMFreeState : OrbSMStateBase
    {
        public override void Enter(Animator animator)
        {
            context.orbMovementBehaviour.ResetMovement();
            context.orbBounceBehaviour.Enable(true);

            foreach (Collider c in context.OrbEntity.GetComponents<Collider>())
            {
                c.enabled = true;
            }
        }
    } 
}