using UnityEngine;
using UnityEngine.Events;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisce l'input del player
    /// </summary>
    public class PlayerInputBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Enumerativo che indica il tipo di movimento che si vuole
        /// </summary>
        public enum DirectionType { Global, Camera };
        public DirectionType InputDirection;

        /// <summary>
        /// Riferimento alla camera
        /// </summary>
        Camera cam;

        /// <summary>
        /// Custom setup del Behaviour
        /// </summary>
        protected override void CustomSetup()
        {
            cam = Camera.main;

            canAttract = true;
            canShoot = true;
            canMove = true;
            canDash = true;

            isControllerConnected = CheckControllerConnection();
        }

        /// <summary>
        /// True se il controlller è connesso, false altrimenti
        /// </summary>
        bool isControllerConnected;
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

        public override void OnUpdate()
        {
            if (IsSetupped)
            {
                isControllerConnected = CheckControllerConnection();

                ReadMovement(isControllerConnected);
                ReadShot(isControllerConnected);
                ReadDash(isControllerConnected);
                ReadAttraction(isControllerConnected);
            }
        }

        #region Movement
        [Header("Movement")]
        /// <summary>
        /// Evento lanciato al cambio di direzione dell'asse di input
        /// </summary>
        [SerializeField] UnityVector3Event OnDirectionUpdate;

        /// <summary>
        ///  Asse che corrisponde al movimento orizzontale per il controller
        /// </summary>
        [SerializeField] string controllerHorizontalInput = "HorizontalLeftController";
        /// <summary>
        /// Asse che corrisponde al movimento verticale per il controller
        /// </summary>
        [SerializeField] string controllerVerticalInput = "VerticalLeftController";

        /// <summary>
        /// Asse che corrisponde al movimento orizzontale per la tastiera
        /// </summary>
        [SerializeField] string keyboardHorizontalInput = "HorizontalKeyboard";
        /// <summary>
        /// Asse che corrisponde al movimento verticale per la tastiera
        /// </summary>
        [SerializeField] string keyboardVerticalInput = "VerticalKeyboard";

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
        /// True se l'abilità di movimento è attiva, false altrimenti
        /// </summary>
        bool canMove;

        /// <summary>
        /// Funzione che attiva o disattiva l'abilità di movimento
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleMovementInput(bool _value)
        {
            canMove = _value;
            MoveDirection = Vector3.zero;
        }

        /// <summary>
        /// Funzione che legge l'input per il movimento
        /// </summary>
        /// <param name="_isControllerConnected"></param>
        void ReadMovement(bool _isControllerConnected)
        {
            if (!canMove)
                return;

            if (_isControllerConnected)
            {
                CalculateMoveDirection(Input.GetAxis(controllerHorizontalInput), Input.GetAxis(controllerVerticalInput));
            }
            else
            {
                CalculateMoveDirection(Input.GetAxisRaw(keyboardHorizontalInput), Input.GetAxisRaw(keyboardVerticalInput));
            }
        }

        /// <summary>
        /// Variabile temporanea per il calcolo del movimento in base alla camera
        /// </summary>
        Vector3 cameraBasedDirection;
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
        #endregion

        #region Shot
        [Header("Shot")]
        /// <summary>
        /// Evento lanciato alla pressione del bottone di shot
        /// </summary>
        [SerializeField] UnityEvent OnShotPressed;

        /// <summary>
        /// Tasto che corrisponde allo shot per il controlller
        /// </summary>
        [SerializeField] string controllerShotInput = "ShotController";
        /// <summary>
        /// Tasto che corrisponde allo shot per la tastiera
        /// </summary>
        [SerializeField] string keyboardShotInput = "ShotKeyboard";

        /// <summary>
        /// True se l'abilità di sparo è attiva, false altrimenti
        /// </summary>
        bool canShoot;

        /// <summary>
        /// Funzione che attiva o disattiva l'abilità di sparare
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleShootInput(bool _value)
        {
            canShoot = _value;
        }

        /// <summary>
        /// Variabile che salva il valore al frame precedente dell'asse di shot
        /// </summary>
        int shotInputPrevValue;
        /// <summary>
        /// Funzione che legge l'input per lo sparo
        /// </summary>
        /// <param name="_isControllerConnected"></param>
        void ReadShot(bool _isControllerConnected)
        {
            if (!canShoot)
                return;

            if (_isControllerConnected)
            {
                int shotAxis = (int)Input.GetAxis(controllerShotInput);
                if (shotAxis == 1 && shotInputPrevValue == 0)
                {
                    OnShotPressed.Invoke();
                    shotInputPrevValue = 1;
                }
                else if (shotAxis == 0 && shotInputPrevValue == 1)
                {
                    shotInputPrevValue = 0;
                }
            }
            else
            {
                if (Input.GetButtonDown(keyboardShotInput))
                    OnShotPressed.Invoke();
            }
        }
        #endregion

        #region Dash
        [Header("Dash")]
        /// <summary>
        /// Evento lanciato alla pressione del bottone di dash
        /// </summary>
        [SerializeField] UnityVector3Event OnDashPressed;

        /// <summary>
        /// Tasto che corrisponde al dash per il controlller
        /// </summary>
        [SerializeField] string controllerDashInput = "DashController";
        /// <summary>
        /// Tasto che corrisponde al dash per la tastiera
        /// </summary>
        [SerializeField] string keyboardDashInput = "DashKeyboard";

        /// <summary>
        /// True se l'abilità di dash è attiva, false altrimenti
        /// </summary>
        bool canDash;

        /// <summary>
        /// Funzione che attiva o disattiva l'abilità di dash
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleDashInput(bool _value)
        {
            canDash = _value;
        }

        /// <summary>
        /// Funzione che legge l'input per il dash
        /// </summary>
        /// <param name="_isControllerConnected"></param>
        void ReadDash(bool _isControllerConnected)
        {
            if (!canDash)
                return;

            if (_isControllerConnected)
            {
                if (Input.GetButtonDown(controllerDashInput))
                    OnDashPressed.Invoke(MoveDirection);
            }
            else
            {
                if (Input.GetButtonDown(keyboardDashInput))
                    OnDashPressed.Invoke(MoveDirection);
            }
        }
        #endregion

        #region Attraction
        [Header("Attraction")]
        /// <summary>
        /// Evento lanciato al mantenimento della pressione del bottone di attraction
        /// </summary>
        [SerializeField] UnityEvent OnAttractionHeld;
        /// <summary>
        /// Evento lanciato al rilascio del bottone di attraction
        /// </summary>
        [SerializeField] UnityEvent OnAttractionReleased;

        /// <summary>
        /// Tasto che corrisponde all'attraction per il controlller
        /// </summary>
        [SerializeField] string controllerAttractionInput = "AttractionController";
        /// <summary>
        /// Tasto che corrisponde all'attraction per la tastiera
        /// </summary>
        [SerializeField] string keyboardAttractionInput = "AttractionKeyboard";

        /// <summary>
        /// True se l'abilità di attrazione è attiva, false altrimenti
        /// </summary>
        bool canAttract;

        /// <summary>
        /// Funzione che attiva o disattiva l'abilità di attrazione
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleAttractInput(bool _value)
        {
            canAttract = _value;
        }

        /// <summary>
        /// Funzione che legge l'input per l'attrazione
        /// </summary>
        /// <param name="_isControllerConnected"></param>
        void ReadAttraction(bool _isControllerConnected)
        {
            if (!canAttract)
                return;

            if (_isControllerConnected)
            {
                if (Input.GetButton(controllerAttractionInput))
                    OnAttractionHeld.Invoke();
                if (Input.GetButtonUp(controllerAttractionInput))
                    OnAttractionReleased.Invoke();
            }
            else
            {
                if (Input.GetButton(keyboardAttractionInput))
                    OnAttractionHeld.Invoke();
                if (Input.GetButtonUp(keyboardAttractionInput))
                    OnAttractionReleased.Invoke();
            }
        }
        #endregion
    }
}
