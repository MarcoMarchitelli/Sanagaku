using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che si occupa di ruotare la transform dell'enità nel punto in cui si fa il raycast 
    /// </summary>
    public class PlayerAimBehaviour : MonoBehaviour, IBehaviour
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
        /// Riferimento alla camera
        /// </summary>
        Camera cam;
        /// <summary>
        /// Layer su cui viene efftuato il raycast
        /// </summary>
        [SerializeField] LayerMask aimLayer;

        /// <summary>
        /// Eseguo il setup del behaviour
        /// </summary>
        /// <param name="_entity"></param>
        /// <param name="_camera"></param>
        public void Setup(IEntity _entity)
        {
            Entity = _entity;
            cam = Camera.main;
            IsSetupped = true;
        }

        void Update()
        {
            if (IsSetupped)
                Aim();
        }

        /// <summary>
        /// Funzione che si occupa della gestione della mira
        /// </summary>
        void Aim()
        {
            if (cam == null)
                return;

            RaycastHit hitInfo;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, int.MaxValue, aimLayer))
            {
                transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
            }
        }
    }
}
