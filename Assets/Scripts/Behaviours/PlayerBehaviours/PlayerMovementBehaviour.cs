using UnityEngine;
using UnityEngine.Events;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisce il movimento del player
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementBehaviour : MonoBehaviour, IBehaviour
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
        /// Riferimento all'entitià che controlla il Behaviour
        /// </summary>
        public IEntity Entity { get; private set; }
        /// <summary>
        /// True se il Behaviour è stato setuppato, false altrimenti
        /// </summary>
        public bool IsSetupped { get; private set; }

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
        /// Eseguo il setup del behaviour
        /// </summary>
        /// <param name="_entity"></param>
        public void Setup(IEntity _entity)
        {
            Entity = _entity;
            rBody = GetComponent<Rigidbody>();
            IsSetupped = true;
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

        void FixedUpdate()
        {
            if (IsSetupped)
                Move();
        }
    } 
}