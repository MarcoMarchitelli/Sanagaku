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

        protected override void CustomSetup()
        {
            ResetMovement();
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

        /// <summary>
        /// Mi salvo il delta time del frame precedente da usare nel caso in cui il delta time corrente sia 0
        /// </summary>
        float prevDeltaTime;
        Vector3 CalculateForwardDirection()
        {
            float timeOfEvaluation = timer / moveTime;
            float evaluatedSpeed = speedOverLifeTimeCurve.Evaluate(timeOfEvaluation) * moveSpeed;

            // se il delta time è zero anche solo per qualche frame la palla smette di muoversi
            if (Time.deltaTime == 0)
            {
                evaluatedSpeed *= prevDeltaTime;
            }
            else
            {
                evaluatedSpeed *= Time.deltaTime;
                prevDeltaTime = Time.deltaTime;
            }

            return Vector3.forward * evaluatedSpeed;
        }

        #region API
        public override void OnUpdate()
        {
            if (Time.timeScale == 0)
            {
                Debug.Log(canMove + " - " + IsSetupped);
                return;
            }

            if (!IsSetupped)
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