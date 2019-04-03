using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Controllore dell'orb
    /// </summary>
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
        /// Riferimento alla grafica
        /// </summary>
        MeshRenderer mesh;
        /// <summary>
        /// Riferimento ai collider
        /// </summary>
        MeshCollider[] colliders;

        /// <summary>
        /// Custom entity Setup with custom parameters.
        /// </summary>
        /// <param name="_playerController"></param>
        public void SetUpOrbEntity(PlayerController _playerController)
        {
            playerReference = _playerController;
            SetUpEntity();
        }

        /// <summary>
        /// Custom setup dell'entità
        /// </summary>
        public override void CustomSetup()
        {
            Data = new OrbControllerData(playerReference);
            if (!SM)
                SM = GetComponentInChildren<OrbSMController>();
            SM.SetUpSM();

            mesh = GetComponentInChildren<MeshRenderer>();
            colliders = GetComponentsInChildren<MeshCollider>();
        }

        /// <summary>
        /// Funzione che attiva o disattiva l'orb
        /// </summary>
        /// <param name="_setActive"></param>
        public void Toggle(bool _setActive)
        {
            mesh.gameObject.SetActive(_setActive);

            for (int i = 0; i < colliders.Length; i++)
                colliders[i].enabled = _setActive;
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

    /// <summary>
    /// Classe che contiene i dati dell'orb
    /// </summary>
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