using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Classe che accende o spegne i renderer tra i figli
    /// </summary>
    public class MeshToggler : MonoBehaviour
    {
        /// <summary>
        /// Funzione che si occupa di accende o spegnere i renderer tra i figli
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleMeshesInChildren(bool _value)
        {
            Renderer[] rends = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < rends.Length; i++)
            {
                rends[i].enabled = _value;
            }
        }
    } 
}
