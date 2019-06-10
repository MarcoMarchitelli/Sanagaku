using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Classe che esegue il compito di controller del player
    /// </summary>
    public class PlayerController : BaseEntity
    {
        AudioListener audioListener;

        /// <summary>
        /// Setup custom della classe
        /// </summary>
        public override void CustomSetup()
        {
            DamageReceiverBehaviour damageReceiver = GetBehaviour<DamageReceiverBehaviour>();
            UI_HealthBehaviour uI_Health = GetBehaviour<UI_HealthBehaviour>();

            if (uI_Health != null)
                uI_Health.CustomSetup(damageReceiver);
            else
                Debug.LogError("***Missing UI_HealthBehaviour***");
        }

        public AudioListener GetAudioListener()
        {
            if(audioListener == null)
                audioListener = GetComponent<AudioListener>();
            return audioListener;
        }
    }
}