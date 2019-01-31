using UnityEngine;

namespace Sangaku
{
    public class TimerBehaviour : MonoBehaviour, IBehaviour
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
        /// Eseguo il setup del behaviour
        /// </summary>
        /// <param name="_entity"></param>
        public void Setup(IEntity _entity)
        {
            Entity = _entity;
            IsSetupped = true;
            if (startCountingOnAwake)
                StartTimer();
        }

        [Multiline] [SerializeField] private string description;
        [SerializeField] private float time;
        [SerializeField] private bool startCountingOnAwake = false;
        [SerializeField] private bool repeat = true;

        public UnityVoidEvent OnTimerStart, OnTimerEnd;

        bool countTime = false;
        [HideInInspector] public float timer;

        #region MonoBehaviour methods

        private void OnEnable()
        {
            if (repeat)
                OnTimerEnd.AddListener(ResetTimer);
            else
                OnTimerEnd.AddListener(StopTimer);
        }

        private void OnDisable()
        {
            if (repeat)
                OnTimerEnd.RemoveListener(ResetTimer);
            else
                OnTimerEnd.RemoveListener(StopTimer);
        }

        void Update()
        {
            if (countTime)
                timer += Time.deltaTime;
            if (timer >= time && countTime)
            {
                OnTimerEnd.Invoke();
                print(name + " ha finito");
            }
        }

        #endregion

        #region API

        public void StartTimer(float _time)
        {
            time = _time;
            countTime = true;
            print(name + " è iniziato");
        }

        public void StartTimer()
        {
            timer = 0;
            countTime = true;
            OnTimerStart.Invoke();
        }

        public void StopTimer()
        {
            timer = 0;
            countTime = false;
        }

        public void PauseTimer()
        {
            countTime = false;
        }

        public void ResetTimer()
        {
            timer = 0;
            if (repeat)
                countTime = true;
            else
                countTime = false;
        }

        public void Say(string _msg)
        {
            Debug.Log(_msg);
        }

        #endregion
    }
}