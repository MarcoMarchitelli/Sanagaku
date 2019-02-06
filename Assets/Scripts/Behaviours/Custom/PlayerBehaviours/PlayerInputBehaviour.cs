using UnityEngine;
using UnityEngine.Events;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisce l'input del player
    /// </summary>
    public class PlayerInputBehaviour : BaseBehaviour
    {
        #region Events
        /// <summary>
        /// Evento lanciato alla pressione del bottone di dash
        /// </summary>
        [SerializeField] UnityVector3Event OnDashPressed;
        /// <summary>
        /// Evento lanciato alla pressione del bottone di shot
        /// </summary>
        [SerializeField] UnityEvent OnShotPressed;
        /// <summary>
        /// Evento lanciato alla pressione del bottone di parry
        /// </summary>
        [SerializeField] UnityEvent OnParryPressed;
        /// <summary>
        /// Evento lanciato al cambio di direzione dell'asse di input
        /// </summary>
        [SerializeField] UnityVector3Event OnDirectionUpdate;
        #endregion

        /// <summary>
        /// Enumerativo che indica il tipo di movimento che si vuole
        /// </summary>
        public enum DirectionType { Global, Camera };
        public DirectionType InputDirection;

        /// <summary>
        /// Direzione in cui viene mosso l'asse di input della direzione
        /// </summary>
        Vector3 _moveDirection;
        /// <summary>
        /// Propery che lancia un evento al cambio di direzione dell'input
        /// </summary>
        Vector3 MoveDirection
        {
            get { return _moveDirection; }
            set
            {
                if (_moveDirection != value)
                {
                    _moveDirection = value;
                    OnDirectionUpdate.Invoke(_moveDirection); 
                }
            }
        }
        /// <summary>
        /// Riferimento alla camera
        /// </summary>
        Camera cam;

        bool canShoot = true;
        public void ToggleShootInput(bool _value)
        {
            canShoot = _value;
        }

        bool canMove = true;
        public void ToggleMovementInput(bool _value)
        {
            canMove = _value;
            MoveDirection = Vector3.zero;
        }

        bool canDash = true;
        public void ToggleDashInput(bool _value)
        {
            canDash = _value;
        }

        bool canParry = true;
        public void ToggleParryInput(bool _value)
        {
            canParry = _value;
        }

        protected override void CustomSetup()
        {
            cam = Camera.main;
        }

        /// <summary>
        /// Update
        /// </summary>
        void Update()
        {
            if (IsSetupped)
            {
                CheckControllerConnection();
                ReadInput();
            }
        }

        bool isControllerConnected;
        /// <summary>
        /// Variabile temporanea per il calcolo del movimento in base alla camera
        /// </summary>
        Vector3 cameraBasedDirection;
        /// <summary>
        /// Funzione che si occupa di leggere gli input
        /// </summary>
        void ReadInput()
        {
            if (isControllerConnected)
                ReadControllerInput();
            else
                ReadKeyboardInput();
        }

        #region Controller Inputs
        [Header("Controller")]
        /// <summary>
        /// Tasto che corrisponde allo shot
        /// </summary>
        [SerializeField] string controllerShotInput = "ShotController";
        /// <summary>
        /// Tasto che corrisponde al parry
        /// </summary>
        [SerializeField] string controllerParryInput = "ParryController";
        /// <summary>
        /// Tasto che corrisponde al dash
        /// </summary>
        [SerializeField] string controllerDashInput = "DashController";
        /// <summary>
        /// Tasto che corrisponde al dash
        /// </summary>
        [SerializeField] string controllerHorizontalInput = "HorizontalController";
        /// <summary>
        /// Tasto che corrisponde al dash
        /// </summary>
        [SerializeField] string controllerVerticalInput = "VerticalController";

        /// <summary>
        /// Variabile che salva il valore al frame precedente dell'asse di shot
        /// </summary>
        int cShotInputPrevValue;
        /// <summary>
        /// Funzione che si occupa di leggere l'input della tastiera
        /// </summary>
        void ReadControllerInput()
        {
            //Move Input
            if (canMove)
                CalculateMoveDirection(Input.GetAxis(controllerHorizontalInput), Input.GetAxis(controllerVerticalInput));

            //Shoot Input
            if (canShoot)
            {
                int shotAxis = (int)Input.GetAxis(controllerShotInput);
                if (shotAxis == 1 && cShotInputPrevValue == 0)
                {
                    OnShotPressed.Invoke();
                    cShotInputPrevValue = 1;
                }
                else if (shotAxis == 0 && cShotInputPrevValue == 1)
                    cShotInputPrevValue = 0;            
            }

            //ParryInput
            if (canParry && Input.GetButtonDown(controllerParryInput))
                OnParryPressed.Invoke();

            //DashInput
            if (canDash && Input.GetButtonDown(controllerDashInput))
                OnDashPressed.Invoke(MoveDirection);
        }

        /// <summary>
        /// Funzione che controlla se è connesso un controller
        /// </summary>
        void CheckControllerConnection()
        {
            string[] controllerNames = Input.GetJoystickNames();

            if (controllerNames.Length == 0)
            {
                isControllerConnected = false;
            }
            else
            {
                for (int i = 0; i < controllerNames.Length; i++)
                {
                    if (!string.IsNullOrEmpty(controllerNames[i]))
                    {
                        isControllerConnected = true;
                        return;
                    }
                }
            }
        }
        #endregion

        #region Keyboard Inputs
        [Header("Keyboard")]
        /// <summary>
        /// Tasto che corrisponde allo shot
        /// </summary>
        [SerializeField] string keyboardShotInput = "ShotKeyboard";
        /// <summary>
        /// Tasto che corrisponde al parry
        /// </summary>
        [SerializeField] string keyboardParryInput = "ParryKeyboard";
        /// <summary>
        /// Tasto che corrisponde al dash
        /// </summary>
        [SerializeField] string keyboardDashInput = "DashKeyboard";
        /// <summary>
        /// Tasto che corrisponde al dash
        /// </summary>
        [SerializeField] string keyboardHorizontalInput = "HorizontalKeyboard";
        /// <summary>
        /// Tasto che corrisponde al dash
        /// </summary>
        [SerializeField] string keyboardVerticalInput = "VerticalKeyboard";

        /// <summary>
        /// Funzione che si occupa di leggere l'input della tastiera
        /// </summary>
        void ReadKeyboardInput()
        {
            //Move Input
            if (canMove)
                CalculateMoveDirection(Input.GetAxisRaw(keyboardHorizontalInput), Input.GetAxisRaw(keyboardVerticalInput));

            //Shoot Input
            if (canShoot && Input.GetButtonDown(keyboardShotInput))
                OnShotPressed.Invoke();

            //ParryInput
            if (canParry && Input.GetButtonDown(keyboardParryInput))
                OnParryPressed.Invoke();

            //DashInput
            if (canDash && Input.GetButtonDown(keyboardDashInput))
                OnDashPressed.Invoke(MoveDirection);
        }
        #endregion

        /// <summary>
        /// Funzione che calcola il vettore di movimento dati due assi di input
        /// </summary>
        /// <param name="_horizontalInput"></param>
        /// <param name="_verticalInput"></param>
        void CalculateMoveDirection(float _horizontalInput, float _verticalInput)
        {
            if (InputDirection == DirectionType.Global)
                MoveDirection = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;
            else if (InputDirection == DirectionType.Camera && cam != null)
            {
                MoveDirection = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;
                cameraBasedDirection = cam.transform.TransformDirection(MoveDirection);
                MoveDirection = new Vector3(cameraBasedDirection.x, MoveDirection.y, cameraBasedDirection.z).normalized;
            }
        }
    }
}
