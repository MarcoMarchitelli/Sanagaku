using UnityEngine;

namespace Sangaku
{
    public class Orb : BaseEntity
    {
        public OrbSMController SM;

        //Transform catchPoint;

        //public void OrbSetUp(Transform _catchPoint)
        //{
        //    catchPoint = _catchPoint;
        //    SetUpEntity();
        //}

        public override void CustomSetup()
        {
            if(!SM)
                SM = GetComponentInChildren<OrbSMController>();
            //SM.OrbSMSetUp(catchPoint);
            SM.SetUpSM();
        }
    }
}