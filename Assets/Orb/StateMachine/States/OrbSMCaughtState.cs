using UnityEngine;
using System.Collections.Generic;

namespace Sangaku
{
    public class OrbSMCaughtState : OrbSMStateBase
    {
        Transform orbTransform;

        public override void Enter()
        {
            ToggleBehaviours(context.OrbEntity.Behaviours, false);

            orbTransform = context.OrbEntity.transform;
            context.OrbManaBehaviour.ResetMana();
        }

        public override void Tick()
        {
            orbTransform.position = context.CatchPoint.position;
            orbTransform.rotation = context.CatchPoint.rotation;
        }

        public override void Exit()
        {
            ToggleBehaviours(context.OrbEntity.Behaviours, true);
            orbTransform = null;
        }

        void ToggleBehaviours(List<IBehaviour> _baseBehaviours, bool _value)
        {
            for (int i = 0; i < _baseBehaviours.Count; i++)
            {
                _baseBehaviours[i].Enable(_value);
            }
        }
    }
}