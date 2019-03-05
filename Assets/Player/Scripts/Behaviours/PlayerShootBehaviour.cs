using UnityEngine;
using System.Collections.Generic;

namespace Sangaku
{
    [RequireComponent(typeof(PlayerManaBehaviour))]
    public class PlayerShootBehaviour : BaseBehaviour
    {
        #region Serialized Fields
        [SerializeField] protected string projectilePool = "Orb";
        [SerializeField] protected Transform shootPoint;
        [Tooltip("How many seconds between each shot.")]
        [SerializeField] protected float secondsBetweenShots;
        [Tooltip("Costo di un proiettile in mana")]
        public float cost;
        #endregion

        #region Events
        [SerializeField] protected UnityFloatEvent OnOrbShoot;
        #endregion

        #region Data
        PlayerManaBehaviour playerMana;
        PlayerOrbInteractionBehaviour orbInteraction;
        #endregion

        List<OrbController> orbsInPlay;

        protected override void CustomSetup()
        {
            playerMana = GetComponent<PlayerManaBehaviour>();
            orbInteraction = GetComponentInChildren<PlayerOrbInteractionBehaviour>();
            orbsInPlay = new List<OrbController>();
        }

        #region API
        public Transform GetShootPoint()
        {
            return shootPoint;
        }

        /// <summary>
        /// Funzione che ritorna la lista di orb in gioco
        /// </summary>
        /// <returns></returns>
        public List<OrbController> GetOrbsInPlay()
        {
            return orbsInPlay;
        }

        /// <summary>
        /// Frees the caught orb.
        /// </summary>
        public void ShootCaughtOrb()
        {
            orbInteraction.FreeOrb();
            OnOrbShoot.Invoke(secondsBetweenShots);
        }

        /// <summary>
        /// Removes an OrbController to the list that tracks all the orbs currently in play.
        /// </summary>
        /// <param name="_orb"></param>
        public void RemoveOrbFromPlay(OrbController _orb)
        {
            if (orbsInPlay.Contains(_orb))
            {
                orbsInPlay.Remove(_orb);
                CheckOrbsInPlay();
            }
        }

        /// <summary>
        /// Shoot logic.
        /// </summary>
        public void Shoot()
        {
            if (orbInteraction.caughtOrb)
            {
                ShootCaughtOrb();
            }
            else
            {
                ShootOrb();
            }
        }
        #endregion

        /// <summary>
        /// Instantiates a new Orb.
        /// </summary>
        void ShootOrb()
        {
            if (playerMana.GetMana() >= cost)
            {
                playerMana.AddMana(-cost);

                //OrbController instantiatedOrb = Instantiate(projectilePrefab.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<OrbController>();
                //instantiatedOrb.SetUpOrbEntity(Entity as PlayerController);

                OrbController pooledOrb = ObjectPooler.Instance.GetPoolableFromPool(projectilePool, shootPoint.position, shootPoint.rotation) as OrbController;
                pooledOrb.SetUpOrbEntity(Entity as PlayerController);
                AddOrbInPlay(pooledOrb);

                OnOrbShoot.Invoke(secondsBetweenShots);
            }
        }

        /// <summary>
        /// Adds an OrbController to the list that tracks all the orbs currently in play.
        /// </summary>
        /// <param name="_orb"></param>
        void AddOrbInPlay(OrbController _orb)
        {
            if (!orbsInPlay.Contains(_orb))
            {
                orbsInPlay.Add(_orb);
                CheckOrbsInPlay();
            }
        }

        void CheckOrbsInPlay()
        {
            if (orbsInPlay.Count == 0 && playerMana.GetMana() < cost)
            {
                playerMana.ToggleRegen(true);
            }
            else
            {
                playerMana.ToggleRegen(false);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(shootPoint.position, 0.3f);
        }
    }
}