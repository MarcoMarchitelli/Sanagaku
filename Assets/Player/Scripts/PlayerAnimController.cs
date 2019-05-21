using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    public class PlayerAnimController : BaseBehaviour
    {
        Animator anim;

        Vector3 direction;

        protected override void CustomSetup()
        {
            anim = GetComponentInChildren<Animator>();
        }

        public void SetDirection(Vector3 _direction)
        {
            direction = _direction;
        }

        void SetAnimation()
        {
            anim.SetFloat("XMov", direction.x);
            anim.SetFloat("YMov", direction.z);
        }

        public override void OnFixedUpdate()
        {
            SetAnimation();
        }

    }

}