using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisc lo sparo
    /// </summary>
    public class ShootBehaviour : BaseBehaviour
    {
        #region Serialized Fields
        [SerializeField] protected BaseEntity projectilePrefab;
        [SerializeField] protected Transform shootPoint;
        [Tooltip("How many seconds between each shot.")]
        [SerializeField] protected float secondsBetweenShots;
        [SerializeField] bool firesOnStart = false;
        #endregion

        #region Events
        [SerializeField] protected UnityFloatEvent OnShoot;
        #endregion

        protected override void CustomSetup()
        {
            if (firesOnStart)
                canShoot = true;
        }

        float timer = 0;
        bool canShoot = false;

        private void Update()
        {
            if (canShoot)
            {
                timer += Time.deltaTime;
                if (timer >= secondsBetweenShots)
                    Shoot();
            }
        }

        /// <summary>
        /// Funzione che instanzia un proiettile
        /// </summary>
        public virtual void Shoot()
        {
            // ------- //TODO objpooler
            BaseEntity instBullet = Instantiate(projectilePrefab.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<BaseEntity>();
            instBullet.SetUpEntity();
            timer = 0;
            // ------- aaaaa
            OnShoot.Invoke(secondsBetweenShots);
        }
    } 
} 