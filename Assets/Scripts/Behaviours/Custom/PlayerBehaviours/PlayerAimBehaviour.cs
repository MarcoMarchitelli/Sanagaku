using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che si occupa di ruotare la transform dell'enità nel punto in cui si fa il raycast 
    /// </summary>
    public class PlayerAimBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Riferimento alla camera
        /// </summary>
        Camera cam;
        /// <summary>
        /// Layer su cui viene efftuato il raycast
        /// </summary>
        [SerializeField] LayerMask aimLayer;

        protected override void CustomSetup()
        {
            cam = Camera.main;
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
