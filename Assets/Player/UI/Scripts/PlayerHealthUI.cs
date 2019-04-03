using UnityEngine;
using UnityEngine.UI;

namespace Sangaku
{
    public class PlayerHealthUI : MonoBehaviour
    {
        /// <summary>
        /// Riferimento all'immagine della barra della vita
        /// </summary>
        [SerializeField] Image fillerImage;

        /// <summary>
        /// Vita massima del player
        /// </summary>
        int maxHealth;

        /// <summary>
        /// Setup della classe
        /// </summary>
        /// <param name="_maxHealth"></param>
        public void Setup(int _maxHealth)
        {
            maxHealth = _maxHealth;
        }

        /// <summary>
        /// Funzione che aggiorna la barra della UI
        /// </summary>
        /// <param name="_currentHealth"></param>
        public void UpdateHealthUI(int _currentHealth)
        {
            float healthPercentage = (_currentHealth * 100f) / maxHealth;
            fillerImage.fillAmount = healthPercentage / 100f;
        }
    } 
}
