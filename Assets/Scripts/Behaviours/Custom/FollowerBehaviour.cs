using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Sangaku
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class FollowerBehaviour : BaseBehaviour
    {
        NavMeshAgent navigation;

        [SerializeField] FollowTarget target;
        [SerializeField] UnityEvent OnTargetReached;
        
        /// <summary>
        /// Custom setup del behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            navigation = GetComponent<NavMeshAgent>();
            canNavigate = false;

            if (target == null)
                target = FindObjectOfType<FollowTarget>();
        }

        bool canNavigate;
        public void ToggleNavigation(bool _value)
        {
            if (!IsSetupped)
                return;

            canNavigate = _value;

            if (target == null)
                return;

            if (canNavigate)
            {
                navigation.destination = target.TargetPosition;
                if (navigation.isActiveAndEnabled)
                    navigation.isStopped = false;
            }
            else
            {
                if (navigation.isActiveAndEnabled)
                    navigation.isStopped = true;
            }         
        }

        private void Update()
        {
            if(navigation.pathStatus == NavMeshPathStatus.PathComplete)
            {
                OnTargetReached.Invoke();
            }
        }
    } 
}
