
namespace Sangaku
{
    public class OrbController : BaseEntity, IPoolable
    {
        /// <summary>
        /// This Orb's state machine.
        /// </summary>
        public OrbSMController SM;

        /// <summary>
        /// Riferimento al player
        /// </summary>
        PlayerController playerReference;

        /// <summary>
        /// Custom entity Setup with custom parameters.
        /// </summary>
        /// <param name="_playerController"></param>
        public void SetUpOrbEntity(PlayerController _playerController)
        {
            playerReference = _playerController;
            SetUpEntity();
        }

        public override void CustomSetup()
        {
            Data = new OrbControllerData(playerReference);
            if (!SM)
                SM = GetComponentInChildren<OrbSMController>();
            SM.SetUpSM();
        }

        #region IPoolable
        public void OnGetFromPool() { }
        public void OnPoolCreation() { }
        public void OnPutInPool()
        {
            Enable(false);
        }
        #endregion
    }

    public class OrbControllerData : IEntityData
    {
        public PlayerController PlayerReference;
        public PlayerShootBehaviour PlayerShootBehaviour;

        public OrbControllerData(PlayerController _p)
        {
            PlayerReference = _p;
            PlayerShootBehaviour = PlayerReference.GetComponentInChildren<PlayerShootBehaviour>();
        }
    }
}