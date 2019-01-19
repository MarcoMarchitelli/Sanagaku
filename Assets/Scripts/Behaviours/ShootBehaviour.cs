using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            IsSetupped = true;
        }

        [SerializeField] protected Transform projectilePrefab;
        [SerializeField] protected Transform shootPoint;
        /// <summary>
        /// How many seconds between each shot.
        /// </summary>
        [Tooltip("How many seconds between each shot.")]
        [SerializeField] protected float fireRate;

        /// <summary>
        /// Funzione che instanzia un proiettile
        /// </summary>
        public virtual void Shoot()
        {
            // ------- NOT FINAL
            Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            // ------- aaaaa
            OnShoot.Invoke(fireRate);
        }

    } 
} 
