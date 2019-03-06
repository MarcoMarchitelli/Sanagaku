using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Player specific behaviour that handles all things mana related.
    /// </summary>
    public class PlayerManaBehaviour : BaseBehaviour
    {
        [SerializeField] float maxMana;
        [SerializeField] float startMana;
        [SerializeField] bool startAtMax = true;
        [SerializeField] bool regeneration = false;
        [SerializeField] bool canExceedMax = false;
        [SerializeField] float amountPerSecond = .1f;

        public UnityFloatEvent OnManaChanged;

        PlayerShootBehaviour playerShootBehaviour;

        protected override void CustomSetup()
        {
            playerShootBehaviour = Entity.gameObject.GetComponentInChildren<PlayerShootBehaviour>();
            if (!playerShootBehaviour)
            {
                Debug.Log(name + "'s PlayerManaBehaviour did not find a PlayerShootBehaviour!");
                return;
            }

            if (startAtMax)
            {
                CurrentMana = maxMana;
                return;
            }

            CurrentMana = startMana;
        }

        float _currentMana = 0;
        float CurrentMana
        {
            get { return _currentMana; }
            set
            {
                if (_currentMana != value)
                {
                    _currentMana = value;
                    if (!canExceedMax && _currentMana > maxMana)
                        _currentMana = maxMana;
                    if (_currentMana > playerShootBehaviour.cost)
                        ToggleRegen(false);
                    OnManaChanged.Invoke(_currentMana);
                }
            }
        }

        public float MaxMana
        {
            get { return maxMana; }
        }

        /// <summary>
        /// Funzione che aggiunge o sottrae mana
        /// </summary>
        /// <param name="_value">Il mana da aggiungere o sottrarre</param>
        /// <returns>true se l'operazione è possibile</returns>
        public void AddMana(float _value)
        {
            float tempMana = CurrentMana;
            tempMana += _value;
            if (tempMana < 0)
                tempMana = 0;
            if (!canExceedMax && tempMana > maxMana)
                tempMana = maxMana;
            CurrentMana = tempMana;
        }

        /// <summary>
        /// Changes the current mana to 0.
        /// </summary>
        public void ResetMana()
        {
            _currentMana = 0;
        }

        /// <summary>
        /// Returns the current mana value.
        /// </summary>
        /// <returns></returns>
        public float GetMana()
        {
            return CurrentMana;
        }

        /// <summary>
        /// Toggles mana regeneration.
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleRegen(bool _value)
        {
            regeneration = _value;
        }

        public override void OnUpdate()
        {
            if (regeneration)
                CurrentMana += amountPerSecond * Time.deltaTime;
        }
    }
}