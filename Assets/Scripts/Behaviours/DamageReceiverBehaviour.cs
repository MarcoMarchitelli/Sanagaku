using UnityEngine;

namespace Sangaku
{
    public class DamageReceiverBehaviour : MonoBehaviour, IBehaviour
    {
        #region Events
        [SerializeField] UnityFloatEvent OnHealthChanged;
        [SerializeField] UnityVoidEvent OnHealthDepleated; 
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
        /// Eseguo il setup del behaviour
        /// </summary>
        /// <param name="_entity"></param>
        public void Setup(IEntity _entity)
        {
            Entity = _entity;
            //CurrentHealth = maxHealth;//TODO: we are not sure about this :)
            IsSetupped = true;
        }

        [SerializeField] float maxHealth;
        float _currentHealth;
        float CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                if (_currentHealth != value)
                {
                    _currentHealth = value;
                    OnHealthChanged.Invoke(_currentHealth);
                    print(name + "'s helth has changed");
                }
            }
        }

        /// <summary>
        /// Funzione che aggiunge o sottrae salute
        /// </summary>
        /// <param name="_value">la salute da aggiungere o sottrarre</param>
        /// <returns>true se l'operazione è possibile</returns>
        public void SetHealth(float _value)
        {
            float tempHealth = CurrentHealth;
            tempHealth += _value;
            if (tempHealth < 0)
            {
                tempHealth = 0;
                OnHealthDepleated.Invoke();
            }
            if (tempHealth > maxHealth)
                tempHealth = maxHealth;
            CurrentHealth = tempHealth;
        }
    } 
}
