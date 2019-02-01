using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che si occupa di eseguire il dash
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class DashBehaviour : BaseBehaviour
    {
        #region Events
        /// <summary>
        /// Evento lanciato all'inizio del dash
        /// </summary>
        [SerializeField] UnityEvent OnDashStart;
        /// <summary>
        /// Evento lanciato alla fine del dash, passa il cooldown del dash
        /// </summary>
        [SerializeField] UnityFloatEvent OnDashEnd;
        #endregion

        /// <summary>
        /// Distanza di dash
        /// </summary>
        [SerializeField] float dashDistance = 10f;
        /// <summary>
        /// Velocitò di dash
        /// </summary>
        [Tooltip("Measured in meters per second")]
        [SerializeField]
        float dashSpeed = 5f;
        /// <summary>
        /// Cooldawn del dash
        /// </summary>
        [SerializeField] float dashCooldown = 3f;


        /// <summary>
        /// Riferimento al Rigidbody
        /// </summary>
        Rigidbody rBody;

        protected override void CustomSetup()
        {
            rBody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Funzione che esegue il dash
        /// </summary>
        public void Dash(Vector3 _dashDirection)
        {
            if (IsSetupped && _dashDirection != Vector3.zero)
            {
                print(_dashDirection);
                //StartCoroutine(DashRoutine(_dashDirection));
                StartDash(_dashDirection);
            }
        }

        ///// <summary>
        ///// Corutine che esegue il dash in Fixed Update
        ///// </summary>
        ///// <returns></returns>
        //IEnumerator DashRoutine(Vector3 _direction)
        //{
        //    float dashTime = dashDistance / dashSpeed;
        //    float timer = 0f;

        //    OnDashStart.Invoke();
        //    print("dash partito");

        //    //perform the dash
        //    while (timer < dashTime)
        //    {
        //        timer += Time.fixedDeltaTime;
        //        //remember we use fixed because we are moving a rigidbody
        //        rBody.MovePosition(transform.position + _direction * dashSpeed * Time.fixedDeltaTime);
        //        yield return new WaitForFixedUpdate();
        //    }

        //    OnDashEnd.Invoke(dashCooldown);
        //    print("dash finito");
        //}

        void StartDash(Vector3 _direction)
        {
            OnDashStart.Invoke();
            rBody.AddForce(_direction * dashSpeed, ForceMode.Impulse);
            StartCoroutine(CountDashTime(dashDistance / dashSpeed));
        }

        IEnumerator CountDashTime(float _time)
        {
            yield return new WaitForSeconds(_time);
            OnDashEnd.Invoke(dashCooldown);
        }

    }
}
