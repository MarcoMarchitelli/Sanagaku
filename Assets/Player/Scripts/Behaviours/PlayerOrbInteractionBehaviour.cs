using UnityEngine;

namespace Sangaku
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerOrbInteractionBehaviour : BaseBehaviour
    {
        [SerializeField] bool drawGizmos = true;
        [SerializeField] bool isEnabled = true;
        [SerializeField] Transform orbCatchPoint;
        [SerializeField] float catchDuration;
        [SerializeField] float catchRadius;

        [SerializeField] UnityFloatEvent OnOrbCatch;
        [SerializeField] UnityVoidEvent OnCatchEnd;

        SphereCollider catchCollider;
        [HideInInspector]
        public OrbController caughtOrb;

        protected override void CustomSetup()
        {
            caughtOrb = null;
            catchCollider = GetComponent<SphereCollider>();
            catchCollider.isTrigger = true;
            catchCollider.radius = catchRadius;
        }

        /// <summary>
        /// Checks if there is no Orb already, and if not catches the given one. Called by the Orb class.
        /// </summary>
        /// <param name="_orb">the Orb to catch.</param>
        public void CatchOrb(OrbController _orb)
        {
            if (!isEnabled)
                return;
            if (!caughtOrb)
            {
                caughtOrb = _orb;
                caughtOrb.SM.GoToCaughtState(orbCatchPoint);
                OnOrbCatch.Invoke(catchDuration);
            }
        }

        /// <summary>
        /// Frees the current caught Orb if there is one.
        /// </summary>
        public void FreeOrb()
        {
            if (!isEnabled)
                return;
            caughtOrb.SM.GoToFreeState();
            caughtOrb = null;
            OnCatchEnd.Invoke();
        }

        public OrbController GetOrb()
        {
            if (!isEnabled)
                return null;
            if (caughtOrb)
                return caughtOrb;
            else
                return null;
        }

        private void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, catchRadius);
            }
        }

        public override void Enable(bool _value)
        {
            isEnabled = _value;
        }

    } 
}