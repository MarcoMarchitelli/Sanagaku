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

        #region Body

        public void SetBodyMovementAnimation()
        {
            SetBodyAnimation(0);
        }

        public void SetBodyIdle()
        {
            SetBodyAnimation(Random.Range(1, 3));
        }

        public void SetDeathAnimation()
        {
            SetBodyAnimation(Random.Range(3, 5));
        }

        void SetBodyAnimation(int _param)
        {
            /// Se il parametro è 0 il character è in moviment.
            /// 1 o 2 sono le due animazioni di idle
            if(anim != null)
            {
                anim.SetInteger("BodyStatus", _param);
            }
        }
        
        #endregion

        #region Movement

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
            if (anim == null)
                return;

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
        #endregion
    }
}