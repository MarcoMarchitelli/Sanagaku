using UnityEngine;

namespace Sangaku
{
    public class OrbController : BaseEntity
    {
        public OrbSMController SM;
        [HideInInspector]
        public PlayerController PlayerReference;

        public override void CustomSetup()
        {
            Data = new OrbControllerData(PlayerReference);
            if(!SM)
                SM = GetComponentInChildren<OrbSMController>();
            SM.SetUpSM();
        }
    }

    public class OrbControllerData : IEntityData
    {
        public PlayerController PlayerReference;

        public OrbControllerData(PlayerController _p)
        {
            PlayerReference = _p;
        }
    }
}