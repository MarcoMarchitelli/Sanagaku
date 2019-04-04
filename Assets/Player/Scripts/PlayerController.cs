using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Classe che esegue il compito di controller del player
    /// </summary>
    public class PlayerController : BaseEntity
    {
        /// <summary>
        /// Riferimento al controller della UI della vita
        /// </summary>
        [SerializeField] PlayerHealthUI healthUI;

        /// <summary>
        /// Setup custom della classe
        /// </summary>
        public override void CustomSetup()
        {
            DamageReceiverBehaviour damageReceiver = GetBehaviour<DamageReceiverBehaviour>();
            if (healthUI != null)
                healthUI.Setup(damageReceiver.GetMaxHealth());
        }
    }
}