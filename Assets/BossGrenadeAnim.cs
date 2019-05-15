using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BossGrenadeAnim : MonoBehaviour
    {
        /// <summary>
        /// Riferimento all'animator
        /// </summary>
        [SerializeField]
        Animator ExpAnimation;

        /// <summary>
        /// Funzione che attiva il dissolve
        /// </summary>
        /// <param name="_other"></param>
        public void StartExpAnimation()
        {
            ExpAnimation.SetBool("PlayExp", true);
        }

        public void ResetExpAnimation()
        {
            ExpAnimation.SetBool("PlayExp", false);
        }

    }   
