using System.Collections;
using System.Collections.Generic;
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
        /// <summary>
        /// Evento lanciato al raggiungimento del target
        /// </summary>
        [SerializeField] UnityEvent OnTargetReached;
        /// <summary>
        /// Evento lanciato al raggiungimento del target
        /// </summary>
        [SerializeField] UnityEvent OnTargetOutOfReach;

        /// <summary>
        /// Riferimento al target da seguire
        /// </summary>
        [SerializeField] FollowerTarget target;
        /// <summary>
        /// Se True il behviour partirà in automatico appena il setup viene eseguito.
        /// Se false si deve chiamare la funzione di toggle.
        /// </summary>
        [SerializeField] bool autoStart;

        /// <summary>
        /// True se il mmovimento è attivo, false altrimenti
        /// </summary>
        bool canNavigate;

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

            if (target == null)
                FindTarget();

            ToggleNavigation(autoStart);
        }

        #region API
        /// <summary>
        /// Funzione che si occupa di spegnere o accendere il movimento 
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleNavigation(bool _value)
        {
            if (!IsSetupped && canNavigate == _value)
                return;

            canNavigate = _value;

            if (target == null && !navigation.isActiveAndEnabled)
                return;

            if (canNavigate)
                navigation.isStopped = false;
            else
                navigation.isStopped = true;
        }

        /// <summary>
        /// Funzion che si occupa di cercare un target in scena
        /// </summary>
        public void FindTarget()
        {
            SetTarget(FindObjectOfType<FollowerTarget>());
        }

        /// <summary>
        /// Funzione che setta il target del behaviour
        /// </summary>
        /// <param name="_target"></param>
        public void SetTarget(FollowerTarget _target)
        {
            target = _target;
        }

        /// <summary>
        /// Funzione che assegna come destinazione della navigazione il target del behaviour
        /// </summary>
        public void SetTargetAsDestination()
        {
            if (target != null)
                navigation.destination = target.TargetPosition;
        } 
        #endregion

        /// <summary>
        /// Funzione che si occupa di controllare il completamento del path
        /// </summary>
        void CheckPath()
        {
            if (!canNavigate)
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
