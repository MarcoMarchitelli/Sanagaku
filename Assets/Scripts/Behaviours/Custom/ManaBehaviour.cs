using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che gestisce l'accumulo di mana
    /// </summary>
    public class ManaBehaviour : BaseBehaviour
    {
        public UnityFloatEvent OnManaChanged;

        protected override void CustomSetup()
        {
            _currentMana = maxMana;
        }

        [SerializeField] float maxMana;
        [SerializeField] bool regeneration = false;
        [SerializeField] float amountPerSecond = .1f;
        float _currentMana = 0;
        float CurrentMana
        {
            get { return _currentMana; }
            set
            {
                if (_currentMana != value)
                {
                    _currentMana = value;
                    if (_currentMana > maxMana)
                        _currentMana = maxMana;
                    OnManaChanged.Invoke(_currentMana);
                }
            }
        }

        /// <summary>
        /// Funzione che aggiunge o sottrae mana
        /// </summary>
        /// <param name="_value">Il mana da aggiungere o sottrarre</param>
        /// <returns>true se l'operazione è possibile</returns>
        public bool SetMana(float _value)
        {
            float tempMana = CurrentMana;
            tempMana += _value;
            if (tempMana < 0)
                return false;
            if (tempMana > maxMana)
                tempMana = maxMana;
            CurrentMana = tempMana;
            return true;
        }

        public float GetMana()
        {
            return maxMana;
        }

        private void Update()
        {
            if(regeneration)
                CurrentMana += amountPerSecond * Time.deltaTime;
        }

    }
}