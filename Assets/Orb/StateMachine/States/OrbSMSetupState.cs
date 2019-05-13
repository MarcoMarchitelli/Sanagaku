using UnityEngine;

namespace Sangaku
{
    public class OrbSMSetupState : OrbSMStateBase
    {
        public override void Enter(Animator animator)
        {
            base.Enter(animator);
            foreach (Collider c in context.OrbEntity.GetComponents<Collider>())
            {
                c.enabled = false;
            }
            animator.SetTrigger("GoToFreeState");
        }

        public override void Exit()
        {
            base.Exit();
            
        }
    }
}