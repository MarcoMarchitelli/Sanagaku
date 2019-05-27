using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    public class EnemyAnimatorBehaviour : BaseBehaviour
    {

        Animator anim;

        protected override void CustomSetup()
        {
            anim = GetComponentInChildren<Animator>();
        }

        public void SetDamageAnimation()
        {
            anim.SetTrigger("TakeDamage");
        }

        public void SetShotAnimation()
        {
            anim.SetTrigger("Shot");
        }
    } 
}
