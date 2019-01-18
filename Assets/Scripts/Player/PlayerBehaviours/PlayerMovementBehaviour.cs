using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisce il movimento del player
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementBehaviour : MonoBehaviour, IBehaviour
    {
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
        /// Rieirmento al Rigidbody
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
        /// <param name="_camera"></param>
        public void Setup(IEntity _entity)
        {
            Entity = _entity;
        }

        /// <summary>
        /// Funzione che setta la direzione di movimento
        /// </summary>
        /// <param name="_moveDirection"></param>
        public void SetMoveDirection(Vector3 _moveDirection)
        {
            moveDirection = _moveDirection;
        }

        /// <summary>
        /// Funzione che gestisce il movimento
        /// </summary>
        /// <param name="_moveDirection"></param>
        void Move()
        {
            rBody.MovePosition(rBody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Fixed Update
        /// </summary>
        void FixedUpdate()
        {
            if (IsSetupped)
                Move();
        }
    } 
}
