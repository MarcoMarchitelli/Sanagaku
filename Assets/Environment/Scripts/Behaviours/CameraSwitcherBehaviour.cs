using UnityEngine;
using Cinemachine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che switacha tra una camera e l'altra
    /// </summary>
    public class CameraSwitcherBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Riferimento alla camera1
        /// </summary>
        [SerializeField] CinemachineVirtualCamera virtualCamera1;
        /// <summary>
        /// Riferimento alla camera2
        /// </summary>
        [SerializeField] CinemachineVirtualCamera virtualCamera2;

        /// <summary>
        /// Camera attualmente attiva
        /// </summary>
        CinemachineVirtualCamera activeVirtualCamera;

        /// <summary>
        /// Customsetup
        /// </summary>
        protected override void CustomSetup()
        {
            activeVirtualCamera = virtualCamera1;
            virtualCamera2.gameObject.SetActive(false);
        }

        /// <summary>
        /// Funzione che switcha tra una camera e l'altra
        /// </summary>
        public void ChangeVirtualCamera()
        {
            if (activeVirtualCamera == virtualCamera1)
            {
                activeVirtualCamera.gameObject.SetActive(false);
                activeVirtualCamera = virtualCamera2;
                activeVirtualCamera.gameObject.SetActive(true);
            }
            else
            {
                activeVirtualCamera.gameObject.SetActive(false);
                activeVirtualCamera = virtualCamera1;
                activeVirtualCamera.gameObject.SetActive(true);
            }
        }
    } 
}
