using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Classe che si occupa di curare il player
    /// </summary>
    public class PlayerCure : MonoBehaviour
    {
        /// <summary>
        /// Funziuione che aggiunge alla vita del player il valore passato
        /// </summary>
        /// <param name="_healtToAdd"></param>
        public void CurePlayer(int _healtToAdd)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if(player != null)
                player.GetBehaviour<DamageReceiverBehaviour>().SetHealth(_healtToAdd);
        } 
    }
}