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
        float distanceToTravel;

        protected override void CustomSetup()
        {
            timer = 0.01f;
            canMove = true;
            countTime = true;
        }

        private void Update()
        {
            if (countTime)
                timer += Time.deltaTime;
            if (canMove)
                Move();
        }

        /// <summary>
        /// Orb movement. Handles slowdown effect as well.
        /// </summary>
        void Move()
        {
            distanceToTravel = speedOverLifeTimeCurve.Evaluate(timer / moveTime) * moveSpeed * Time.deltaTime;
            if (distanceToTravel <= 0)
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

        public void StopMoveTimer()
        {
            timer = 0;
            countTime = false;
        }
    }
}