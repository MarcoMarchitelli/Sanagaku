using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che si occupa di recuperare le palline all'uscita di una stanza
    /// </summary>
    public class RecoverOrbsBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Funzione che si occupa di distruggere tutti gli orb
        /// </summary>
        /// <param name="_other"></param>
        public void RecoverOrb(Collider _other)
        {
            PlayerController player = _other.GetComponent<PlayerController>();
            if(player != null)
            {
                List<OrbController> orbs =  player.GetBehaviour<PlayerShootBehaviour>().GetOrbsInPlay();
                for (int i = 0; i < orbs.Count; i++)
                {
                    orbs[i].GetBehaviour<OrbDestroyBehaviour>().Destroy();
                }
            }
        }
    } 
}
