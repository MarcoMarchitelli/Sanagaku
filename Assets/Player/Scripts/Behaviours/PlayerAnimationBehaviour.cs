using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour del player che gestice le animazioni
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Riferimento all'animator
        /// </summary>
        Animator anim;
        /// <summary>
        /// Direzione attuale del movimento
        /// </summary>
        Vector3 direction;

        /// <summary>
        /// Custom setup del behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            anim = GetComponent<Animator>();
        }

        /// <summary>
        /// Funzione che setta la direzione del movimento
        /// </summary>
        /// <param name="_direction"></param>
        public void SetDirection(Vector3 _direction)
        {
            direction = _direction;
        }

        /// <summary>
        /// Funzione che setta l'animazione di movimento in base alla direzione
        /// </summary>
        void SetMovingAnimation()
        {
            anim.SetFloat("XMov", direction.x);
            anim.SetFloat("YMov", direction.z);
        }

        /// <summary>
        /// Override del fixed update
        /// </summary>
        public override void OnFixedUpdate()
        {
            SetMovingAnimation();
        }
    }
}