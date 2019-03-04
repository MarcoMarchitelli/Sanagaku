using UnityEngine;

namespace Sangaku
{
    public class OrbMovementBehaviour : BaseBehaviour
    {
        #region Events
        [SerializeField] UnityFloatEvent OnLifeEnd;
        #endregion

        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float moveTime = 5f;
        [SerializeField] float deathTime = 2f;
        [SerializeField] AnimationCurve speedOverLifeTimeCurve;

        bool canMove = true;
        bool countTime = true;
        float timer;

        Vector3 offsetDirection;

        protected override void CustomSetup()
        {
            timer = 0.01f;
            canMove = true;
            countTime = true;
            offsetDirection = Vector3.zero;
        }

        /// <summary>
        /// Orb movement. Handles slowdown effect as well.
        /// </summary>
        void Move()
        {
            Vector3 direction = CalculateForwardDirection();

            if (direction.sqrMagnitude <= 0)
            {
                OnLifeEnd.Invoke(deathTime);
                canMove = false;
                return;
            }
            transform.Translate(direction);
        }

        Vector3 CalculateForwardDirection()
        {
            return Vector3.forward * (speedOverLifeTimeCurve.Evaluate(timer / moveTime) * moveSpeed * Time.deltaTime);
        }

        #region API
        public override void OnUpdate()
        {
            if (!IsSetupped || Time.timeScale == 0)
                return;

            if (countTime)
                timer += Time.deltaTime;
            if (canMove)
                Move();
        }

        public void SetEulerAngles(Vector3 _newDirection)
        {
            transform.eulerAngles = _newDirection;
        }

        public void StopMoveTimer()
        {
            timer = 0;
            countTime = false;
        }

        public void ResetMovement()
        {
            timer = 0.01f;
            canMove = true;
            countTime = true;
        }
        #endregion
    }
}