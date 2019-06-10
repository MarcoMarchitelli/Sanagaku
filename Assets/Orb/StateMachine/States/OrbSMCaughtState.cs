using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Stato dell'orb che definisce la fase di cattura
    /// </summary>
    public class OrbSMCaughtState : OrbSMStateBase
    {
        /// <summary>
        /// Riferimento alla tranform dell'orb
        /// </summary>
        Transform orbTransform;
        /// <summary>
        /// Riferimento all'entità dell'orb
        /// </summary>
        OrbController orb;

        public override void Enter()
        {
            orb = context.OrbEntity as OrbController;
            orbTransform = orb.transform;

            foreach (BaseBehaviour baseBehaviour in orb.Behaviours)
                baseBehaviour.Enable(false);

            context.orbManaBehaviour.ResetMana();

            orb.Toggle(false);
        }

        public override void Tick()
        {
            if(orbTransform != null && context != null && context.CatchPoint != null)
            {
                orbTransform.position = context.CatchPoint.position;
                orbTransform.rotation = context.CatchPoint.rotation;
            }
        }

        public override void Exit()
        {
            orb.Toggle(true);

            context.orbMovementBehaviour.SetEulerAngles(context.CatchPoint.eulerAngles);

            foreach (BaseBehaviour baseBehaviour in orb.Behaviours)
                baseBehaviour.Enable(true);

            orbTransform = null;
        }
    }
}