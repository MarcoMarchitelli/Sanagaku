using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che si occupa di eseguire il dash
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class DashBehaviour : MonoBehaviour, IBehaviour
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
        /// Riferimento all'entitià che controlla il Behaviour
        /// </summary>
        public IEntity Entity { get; private set; }
        /// <summary>
        /// True se il Behaviour è stato setuppato, false altrimenti
        /// </summary>
        public bool IsSetupped { get; private set; }

        /// <summary>
        /// Distanza di dash
        /// </summary>
        [SerializeField] float dashDistance = 10f;
        /// <summary>
        /// Velocitò di dash
        /// </summary>
        [Tooltip("Measured in meters per second")]
        [SerializeField] float dashSpeed = 5f;
        /// <summary>
        /// Cooldawn del dash
        /// </summary>
        [SerializeField] float dashCooldown = 3f;


        /// <summary>
        /// Riferimento al Rigidbody
        /// </summary>
        Rigidbody rBody;

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
        /// Funzione che esegue il dash
        /// </summary>
        public void Dash(Vector3 _dashDirection)
        {
            if (IsSetupped && _dashDirection != Vector3.zero)
            {
                StartCoroutine(DashRoutine(_dashDirection));
            }
        }

        /// <summary>
        /// Corutine che esegue il dash in Fixed Update
        /// </summary>
        /// <returns></returns>
        IEnumerator DashRoutine(Vector3 _direction)
        {
            OnDashStart.Invoke();

            Vector3 targetPos = rBody.position + _direction * dashDistance;
            WaitForFixedUpdate wfu = new WaitForFixedUpdate();

            //perform the dash
            while (rBody.position != targetPos)
            {
                //remember we use fixed because we are moving a rigidbody
                rBody.position = Vector3.MoveTowards(rBody.position, targetPos, dashSpeed * Time.fixedDeltaTime);
                yield return wfu;
            }

            OnDashEnd.Invoke(dashCooldown);
        }

    }
}
