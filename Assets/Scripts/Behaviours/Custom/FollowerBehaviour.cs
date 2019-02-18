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
        /// Se True il behviour partirà in automatico appena il setup viene eseguito.
        /// Se false si deve chiamare la funzione di toggle.
        /// </summary>
        [SerializeField] bool autoStart = true;
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
        /// True se il behaviour è attivo, false altrimenti
        /// </summary>
        bool isEnable = false;

        /// <summary>
        /// Custom setup del behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            navigation = GetComponent<NavMeshAgent>();

            if (autoFindTarget)
                FindTarget();

            Enable(autoStart);
        }

        #region API
        /// <summary>
        /// Funzione che si occupa di accendere o spegnere il behaviour
        /// </summary>
        /// <param name="_value"></param>
        public override void Enable(bool _value)
        {
            //Debug.Log(_value);
            //if (!IsSetupped || isEnable == _value)
            //    return;

            isEnable = _value;
            Debug.Log(isEnable);
            //if (!navigation.isActiveAndEnabled)
            //    return;

            navigation.enabled = isEnable;
        }

        /// <summary>
        /// Funzion che si occupa di cercare un target in scena
        /// </summary>
        public void FindTarget()
        {
            if (!IsSetupped)
                return;

            SetTarget(FindObjectOfType<FollowerTarget>());
        }

        /// <summary>
        /// Funzione che setta il target del behaviour
        /// </summary>
        /// <param name="_target"></param>
        public void SetTarget(FollowerTarget _target)
        {
            if (!IsSetupped)
                return;

            target = _target;
        }

        /// <summary>
        /// Funzione che assegna come destinazione della navigazione il target del behaviour
        /// </summary>
        public void SetTargetAsDestination()
        {
            if (IsSetupped && isEnable && target != null)
                navigation.destination = target.TargetPosition;
        }
        #endregion

        /// <summary>
        /// Funzione che si occupa di controllare il completamento del path
        /// </summary>
        void CheckPath()
        {
            if (!isEnable || target == null)
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
