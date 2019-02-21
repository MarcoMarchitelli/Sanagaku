using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Sangaku
{
    /// <summary>
    /// Behavior che esegue il movimento verso un target specifico tramite nav mesh
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class FollowerBehaviour : BaseBehaviour
    {
        #region Events
        [Header("Events", order = 1)]
        /// <summary>
        /// Evento lanciato al raggiungimento del target
        /// </summary>
        [SerializeField] UnityEvent OnTargetReached;
        /// <summary>
        /// Evento lanciato al raggiungimento del target
        /// </summary>
        [SerializeField] UnityEvent OnTargetOutOfReach;
        #endregion

        #region Parameters
        [Header("Parameters", order = 2)]
        /// <summary>
        /// Riferimento al target da seguire
        /// </summary>
        [SerializeField] FollowerTarget target;
        /// <summary>
        /// Se True il behviour cercherà in automatico un target appena il setup viene eseguito.
        /// Se false si deve chiamare la funzione di SetTarget.
        /// </summary>
        [SerializeField] bool autoFindTarget = true;
        #endregion

        /// <summary>
        /// Riferimento al componente di unity che si occupa della navigazione
        /// </summary>
        NavMeshAgent navigation;

        /// <summary>
        /// Custom setup del behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            navigation = GetComponent<NavMeshAgent>();

            if (autoFindTarget)
                target = FindObjectOfType<FollowerTarget>();
        }

        /// <summary>
        /// Funzione che assegna come destinazione della navigazione il target del behaviour
        /// </summary>
        public void SetTargetAsDestination()
        {
            if (IsSetupped && target != null)
                navigation.destination = target.TargetPosition;
        }

        /// <summary>
        /// Funzione che si occupa di controllare il completamento del path
        /// </summary>
        void CheckPath()
        {
            if (!IsSetupped || target == null)
                return;

            if (Vector3.Distance(navigation.destination, target.TargetPosition) > navigation.stoppingDistance)
                OnTargetOutOfReach.Invoke();
            else if (Vector3.Distance(navigation.destination, target.TargetPosition) <= navigation.stoppingDistance)
                OnTargetReached.Invoke();
        }

        void Update()
        {
            CheckPath();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (navigation != null)
                Gizmos.DrawWireSphere(transform.position, navigation.stoppingDistance);
        }
    }
}
