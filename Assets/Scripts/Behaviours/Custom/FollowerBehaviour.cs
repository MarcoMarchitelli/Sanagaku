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
        /// Riferimento al target da seguire
        /// </summary>
        [SerializeField] FollowerTarget target;
        /// <summary>
        /// Se True il behviour partirà in automatico appena il setup viene eseguito.
        /// Se false si deve chiamare la funzione di toggle.
        /// </summary>
        [SerializeField] bool autoStart;

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

            ToggleNavigation(autoStart);

            if (target == null)
                target = FindObjectOfType<FollowerTarget>();
        }

        /// <summary>
        /// True se il mmovimento è attivo, false altrimenti
        /// </summary>
        bool canNavigate;
        /// <summary>
        /// Funzione che si occupa di spegnere o accendere il movimento 
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleNavigation(bool _value)
        {
            if (!IsSetupped)
                return;

            canNavigate = _value;

            if (target == null)
                return;

            if (canNavigate)
            {
                if (navigation.isActiveAndEnabled)
                    navigation.isStopped = false;
            }
            else
            {
                if (navigation.isActiveAndEnabled)
                    navigation.isStopped = true;
            }
        }

        void UpdateDestination()
        {
            if (canNavigate && Vector3.Distance(navigation.destination, target.TargetPosition) > navigation.stoppingDistance)
                navigation.destination = target.TargetPosition;
        }

        /// <summary>
        /// Funzione che si occupa di controllare il completamento del path
        /// </summary>
        void CheckPathCompletition()
        {
            if (navigation.pathStatus == NavMeshPathStatus.PathComplete)
            {
                OnTargetReached.Invoke();
            }
        }

        void Update()
        {
            CheckPathCompletition();
            UpdateDestination();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (navigation != null)
                Gizmos.DrawWireSphere(transform.position, navigation.stoppingDistance);
        }
    }
}
