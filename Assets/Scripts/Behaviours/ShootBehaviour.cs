using UnityEngine;
using Deirin.StateMachine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisc lo sparo
    /// </summary>
    public class ShootBehaviour : MonoBehaviour, IBehaviour
    {
        #region Events
        [SerializeField] protected UnityFloatEvent OnShoot;
        #endregion

        /// <summary>
        /// Riferimento all'entitià che controlla il Behaviour
        /// </summary>
        public IEntity Entity { get; protected set; }
        /// <summary>
        /// True se il Behaviour è stato setuppato, false altrimenti
        /// </summary>
        public bool IsSetupped { get; protected set; }

        /// <summary>
        /// Eseguo il setup del behaviour
        /// </summary>
        /// <param name="_entity"></param>
        public virtual void Setup(IEntity _entity)
        {
            Entity = _entity;
            if (firesOnStart)
                canShoot = true;
            IsSetupped = true;
            print(name + " entity setupped!");
        }

        [SerializeField] protected StateMachineBase projectilePrefab;
        [SerializeField] protected Transform shootPoint;
        /// <summary>
        /// How many seconds between each shot.
        /// </summary>
        [Tooltip("How many seconds between each shot.")]
        [SerializeField] protected float secondsBetweenShots;
        [SerializeField] protected bool firesOnStart = false;

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
            StateMachineBase instBullet = Instantiate(projectilePrefab.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<StateMachineBase>();
            instBullet.SetUpSM();
            timer = 0;
            // ------- aaaaa
            OnShoot.Invoke(secondsBetweenShots);
        }

    } 
} 
