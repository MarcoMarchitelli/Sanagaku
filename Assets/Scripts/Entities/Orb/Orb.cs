using UnityEngine;

namespace Sangaku
{
    public class Orb : BaseEntity, IPoolable
    {
        public OrbSMController SM;

        public override void CustomSetup()
        {
            if(!SM)
                SM = GetComponentInChildren<OrbSMController>();
            SM.SetUpSM();
        }

        public void OnGetFromPool()
        {
            SM.GoToFreeState();
        }

        public void OnPoolCreation()
        {
            SetUpEntity();
        }

        public void OnPutInPool()
        {
            SM.GoToIdleState();
        }
    }
}