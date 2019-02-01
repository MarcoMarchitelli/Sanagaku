using UnityEngine;

namespace Sangaku
{
    [RequireComponent(typeof(ManaBehaviour))]
    public class PlayerShootBehaviour : BaseBehaviour
    {
        #region Serialized Fields
        [SerializeField] protected BaseEntity projectilePrefab;
        [SerializeField] protected Transform shootPoint;
        [Tooltip("How many seconds between each shot.")]
        [SerializeField] protected float secondsBetweenShots;
        [Tooltip("Costo di un proiettile in mana")]
        [SerializeField] float cost;
        #endregion

        #region Events
        [SerializeField] protected UnityFloatEvent OnOrbShoot;
        [SerializeField] protected UnityFloatEvent OnCaughtOrbShoot;
        #endregion

        ManaBehaviour mana;
        PlayerOrbInteractionBehaviour orbInteraction;

        protected override void CustomSetup()
        {
            mana = GetComponent<ManaBehaviour>();
            orbInteraction = GetComponentInChildren<PlayerOrbInteractionBehaviour>();
        }

        /// <summary>
        /// Checks for any caught Orb and shoots it, if he doesn't find one it uses mana to instantiate one.
        /// </summary>
        public void Shoot()
        {
            if(orbInteraction && orbInteraction.FreeOrb())
            {
                OnCaughtOrbShoot.Invoke(secondsBetweenShots);
                return;
            }

            if (mana.SetMana(-cost))
            {
                //----- ObjPooler wating room
                Orb instantiatedOrb = Instantiate(projectilePrefab.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Orb>();
                instantiatedOrb.SetUpEntity();
                OnOrbShoot.Invoke(secondsBetweenShots);
            }
        }
    }
}