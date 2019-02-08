using UnityEngine;

namespace Sangaku
{
    public class OrbController : BaseEntity
    {
        public OrbSMController SM;

        public override void CustomSetup()
        {
            if(!SM)
                SM = GetComponentInChildren<OrbSMController>();
            SM.SetUpSM();
        }
    }
}