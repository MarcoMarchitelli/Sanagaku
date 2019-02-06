using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che si occupa di ruotare la transform dell'enità nel punto in cui si fa il raycast 
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAimBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Layer su cui viene efftuato il raycast
        /// </summary>
        [SerializeField] LayerMask aimLayer;
        /// <summary>
        /// Asse che corrisponde alla mira orizzontale per il controller
        /// </summary>
        [SerializeField] string controllerHorizontalInput = "HorizontalRightController";
        /// <summary>
        /// Asse che corrisponde alla mira verticale per il controller
        /// </summary>
        [SerializeField] string controllerVerticalInput = "VerticalRightController";

        /// <summary>
        /// Riferimento alla camera
        /// </summary>
        Camera cam;
        /// <summary>
        /// Riferimento al rigibbody
        /// </summary>
        Rigidbody rBody;

        /// <summary>
        /// Setup custom del behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            cam = Camera.main;
            rBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (IsSetupped)
            {
                if (CheckControllerConnection())
                    AimWithController();
                else
                    AimWithMouse();
            }
        }

        /// <summary>
        /// Funzione che si occupa della gestione della mira con il controller
        /// </summary>
        void AimWithController()
        {
            if (cam == null)
                return;

            //HACK: va rivisto il calcolo per migliorare il feeling
            Vector3 lookDirection = new Vector3(Input.GetAxis(controllerHorizontalInput), 0f, Input.GetAxis(controllerVerticalInput)).normalized;
            Debug.Log(lookDirection);
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, transform.up);
            rBody.MoveRotation(lookRotation);
        }

        /// <summary>
        /// Funzione che si occupa della gestione della mira con il mouse
        /// </summary>
        void AimWithMouse()
        {
            if (cam == null)
                return;

            RaycastHit hitInfo;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, int.MaxValue, aimLayer))
                transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
        }

        /// <summary>
        /// Funzione che controlla se è connesso un controller
        /// </summary>
        /// <returns>Ritorna true se è connesso, false altrimenti</returns>
        bool CheckControllerConnection()
        {
            string[] controllerNames = Input.GetJoystickNames();

            if (controllerNames.Length == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < controllerNames.Length; i++)
                {
                    if (!string.IsNullOrEmpty(controllerNames[i]))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
