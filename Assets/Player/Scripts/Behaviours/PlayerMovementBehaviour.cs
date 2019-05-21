using UnityEngine;
using UnityEngine.Events;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisce il movimento del player
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementBehaviour : BaseBehaviour
    {
        #region Events
        /// <summary>
        /// Evento lanciato all'inzio del movimento
        /// </summary>
        [SerializeField] UnityEvent OnMovementStart;
        /// <summary>
        /// Evento lanciato alla fine del movimento
        /// </summary>
        [SerializeField] UnityEvent OnMovementStop;
        #endregion

        /// <summary>
        /// Velocità di movimento del Behaviour
        /// </summary>
        [SerializeField] float moveSpeed;

        /// <summary>
        /// Riferimento al Rigidbody
        /// </summary>
        Rigidbody rBody;
        /// <summary>
        /// Direzione del movimento
        /// </summary>
        Vector3 moveDirection;
        /// <summary>
        /// Script responsabile del settaggio dell'animazione
        /// </summary>
        PlayerAnimController animController;

        protected override void CustomSetup()
        {
            rBody = GetComponent<Rigidbody>();
            animController = Entity.GetBehaviour<PlayerAnimController>();
        }

        /// <summary>
        /// Funzione che setta la direzione di movimento
        /// </summary>
        /// <param name="_moveDirection"></param>
        public void SetMoveDirection(Vector3 _moveDirection)
        {
            moveDirection = _moveDirection;

            if (moveDirection == Vector3.zero)
                OnMovementStop.Invoke();
            else
                OnMovementStart.Invoke();
        }

        /// <summary>
        /// Funzione che gestisce il movimento
        /// </summary>
        /// <param name="_moveDirection"></param>
        void Move()
        {
            rBody.MovePosition(rBody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        void SetAnimationDirection()
        {
            animController.SetDirection(moveDirection);
        }

        public override void OnFixedUpdate()
        {
            if (IsSetupped)
            {
                Move();
                SetAnimationDirection();
            }
        }

        public float GetmoveSpeed()
        {
            return moveSpeed;
        }

    } 
}