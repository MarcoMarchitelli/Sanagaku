using UnityEngine;
using System.Collections;

namespace Sangaku
{
    /// <summary>
    /// Stato che definisce la fase di cattura dell'orb
    /// </summary>
    public class OrbSMCaughtState : OrbSMStateBase
    {
        /// <summary>
        /// Riferimento alla tranform dell'orb
        /// </summary>
        Transform orbTransform;
        /// <summary>
        /// Riferimento all'entià dell'orb
        /// </summary>
        OrbController orb;

        public override void Enter()
        {
            orb = context.OrbEntity as OrbController;
            orbTransform = orb.transform;

            foreach (BaseBehaviour baseBehaviour in context.OrbEntity.Behaviours)
                baseBehaviour.Enable(false);

            context.orbDestroyBehaviour.Destroy();

            context.orbManaBehaviour.ResetMana();
        }
    }
}