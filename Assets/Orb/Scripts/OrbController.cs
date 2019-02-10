using UnityEngine;

namespace Sangaku
{
    public class OrbController : BaseEntity, IPoolable
    {
        /// <summary>
        /// This Orb's state machine.
        /// </summary>
        public OrbSMController SM;

        PlayerController PlayerReference;

        public override void CustomSetup()
        {
            Data = new OrbControllerData(PlayerReference);
            if (!SM)
                SM = GetComponentInChildren<OrbSMController>();
            SM.SetUpSM();
        }

        #region IPoolable
        public void OnGetFromPool()
        {

        }

        public void OnPoolCreation()
        {

        }

        public void OnPutInPool()
        {

        }
        #endregion

        /// <summary>
        /// Custom entity Setup with custom parameters.
        /// </summary>
        /// <param name="_p"></param>
        public void SetUpOrbEntity(PlayerController _p)
        {
            PlayerReference = _p;
            SetUpEntity();
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