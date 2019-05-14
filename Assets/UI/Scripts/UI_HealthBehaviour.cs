using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sangaku
{
    public class UI_HealthBehaviour : BaseBehaviour
    {
        DamageReceiverBehaviour damageReceiver;
        List<Image> healthImages = new List<Image>();

        public void CustomSetup(DamageReceiverBehaviour demageBehaviour)
        {
            AddHealthBars();

            if (!damageReceiver && demageBehaviour)
                damageReceiver = demageBehaviour;

            damageReceiver.OnHealthChanged.AddListener(UpdateUI);
        }

        public override void OnLateUpdate()
        {
            transform.rotation = Quaternion.Euler(-90, 0, 180);
        }

        /// <summary>
        /// Prende le barre della vita figlie e le salva (esclude la prima immagine in quanto background
        /// </summary>
        void AddHealthBars()
        {
            Image[] images = GetComponentsInChildren<Image>();

            for (int i = 1; i < images.Length; i++)
                healthImages.Add(images[i]);
        }
        
        /// <summary>
        /// spegne e accende le barre della vita
        /// </summary>
        /// <param name="_newHealth"></param>
        public void UpdateUI(int _newHealth)
        {
            for (int i = 0; i < healthImages.Count; i++)
            {
                if (i >= _newHealth)
                {
                    healthImages[i].enabled = false;
                }
                else
                {
                    healthImages[i].enabled = true;
                }
            }
        }

    }
}