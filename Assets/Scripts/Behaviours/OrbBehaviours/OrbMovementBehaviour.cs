using UnityEngine;
using UnityEngine.Events;

namespace Sangaku
{
    public class OrbMovementBehaviour : MonoBehaviour, IBehaviour
    {
        #region Events
        [SerializeField] UnityFloatEvent OnLifeEnd;
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
        /// Eseguo il setup del behaviour
        /// </summary>
        /// <param name="_entity"></param>
        public void Setup(IEntity _entity)
        {
            Entity = _entity;
            IsSetupped = true;
        }

        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float moveTime = 5f;
        [SerializeField] float deathTime = 2f;
        [SerializeField] AnimationCurve speedOverLifeTimeCurve;

        bool canMove = true;
        float timer = 0;
        float distanceToTravel;

        private void Update()
        {
            timer += Time.deltaTime;
            if(canMove)
                Move();
        }

        /// <summary>
        /// Orb movement. Handles slowdown effect as well.
        /// </summary>
        void Move()
        {
            distanceToTravel = speedOverLifeTimeCurve.Evaluate(timer / moveTime) * moveSpeed * Time.deltaTime;
            if(distanceToTravel <= 0)
            {
                OnLifeEnd.Invoke(deathTime);
                canMove = false;
                return;
            }
            transform.Translate(Vector3.forward * distanceToTravel);
        }

        public void SetEulerAngles(Vector3 _newDirection)
        {
            transform.eulerAngles = _newDirection;
        }
    } 
}