using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Behaviour che si occupa di conteggiare
    /// </summary>
    public class Counter : MonoBehaviour
    {
        /// <summary>
        /// Confronti possibili
        /// </summary>
        public enum ComparisonType { Equals, Greater, Lesser, GreaterEquals, LesserEquals }

        /// <summary>
        /// Descrizione del counter
        /// </summary>
        [Multiline] [SerializeField] public string description;
        /// <summary>
        /// Lista di confronti da eseguire
        /// </summary>
        [SerializeField] List<CounterGroup> counters = new List<CounterGroup>();

        /// <summary>
        /// Funzione che aggiunge al contatore il numero passato
        /// </summary>
        /// <param name="_number"></param>
        public void Add(int _number)
        {
            for (int i = 0; i < counters.Count; i++)
            {
                counters[i].Counter += _number;
                DebugCounter(counters[i]);
            }

            CheckAll();
        }

        /// <summary>
        /// Funzione che sottrae al contatore il numero passato
        /// </summary>
        /// <param name="_number"></param>
        public void Subtract(int _number)
        {
            for (int i = 0; i < counters.Count; i++)
            {
                counters[i].Counter -= _number;
                DebugCounter(counters[i]);
            }

            CheckAll();
        }

        /// <summary>
        /// Funzione che incrementa di uno il contatore
        /// </summary>
        public void Increase()
        {
            for (int i = 0; i < counters.Count; i++)
            {
                counters[i].Counter++;
                DebugCounter(counters[i]);
            }

            CheckAll();
        }

        /// <summary>
        /// Funzione che decrementa di uno il contatore
        /// </summary>
        public void Decrease()
        {
            for (int i = 0; i < counters.Count; i++)
            {
                counters[i].Counter--;
                DebugCounter(counters[i]);
            }

            CheckAll();
        }

        /// <summary>
        /// Funzione che si occupa di resettare i counter completi con auto restart a true
        /// </summary>
        public void ResetCounter()
        {
            for (int i = 0; i < counters.Count; i++)
            {
                if (counters[i].IsComplete && counters[i].AutoRestart)
                {
                    counters[i].IsComplete = false;
                    counters[i].Counter = 0;
                }
            }
        }

        /// <summary>
        /// Funzione che si occupa di eseguire il controllo di tutti i counters
        /// </summary>
        void CheckAll()
        {
            for (int i = 0; i < counters.Count; i++)
            {
                if (!counters[i].IsComplete)
                    CheckCounter(counters[i]);
            }

            ResetCounter();
        }

        /// <summary>
        /// Funzione che si occupa di esegure la comparazione di un counter
        /// </summary>
        void CheckCounter(CounterGroup _counter)
        {
            switch (_counter.Comparison)
            {
                case ComparisonType.Equals:
                    if (_counter.Counter == _counter.ComparisonNumber)
                    {
                        _counter.OnComparisonSucceded.Invoke();
                        _counter.IsComplete = true;
                    }
                    break;
                case ComparisonType.Greater:
                    if (_counter.Counter > _counter.ComparisonNumber)
                    {
                        _counter.OnComparisonSucceded.Invoke();
                        _counter.IsComplete = true;
                    }
                    break;
                case ComparisonType.Lesser:
                    if (_counter.Counter < _counter.ComparisonNumber)
                    {
                        _counter.OnComparisonSucceded.Invoke();
                        _counter.IsComplete = true;
                    }
                    break;
                case ComparisonType.GreaterEquals:
                    if (_counter.Counter >= _counter.ComparisonNumber)
                    {
                        _counter.OnComparisonSucceded.Invoke();
                        _counter.IsComplete = true;
                    }
                    break;
                case ComparisonType.LesserEquals:
                    if (_counter.Counter <= _counter.ComparisonNumber)
                    {
                        _counter.OnComparisonSucceded.Invoke();
                        _counter.IsComplete = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// Funzione che scrive il debug di un counter
        /// </summary>
        /// <param name="_counter"></param>
        void DebugCounter(CounterGroup _counter)
        {
            if (_counter.DebugMode)
                Debug.LogFormat("Object {0}, ID {1} Count : {2}/{2}", gameObject.name, _counter.ID, _counter.Counter, _counter.ComparisonNumber);
        }

        /// <summary>
        /// Classe che definisce un confronto
        /// </summary>
        [System.Serializable]
        class CounterGroup
        {
            /// <summary>
            /// Evento lanciato al successo del confronto scelto
            /// </summary>
            public UnityVoidEvent OnComparisonSucceded;
            /// <summary>
            /// True se il counter si resetta da solo dopo il completamente, false altrimenti
            /// </summary>
            public bool AutoRestart;
            /// <summary>
            /// Id del counter
            /// </summary>
            public string ID;
            /// <summary>
            /// Tipo di confronto scelto
            /// </summary>
            public ComparisonType Comparison;
            /// <summary>
            /// Numero di confrnto
            /// </summary>
            public int ComparisonNumber;
            /// <summary>
            /// True se si deve debuggare il numero del counter, false altrimenti
            /// </summary>
            public bool DebugMode;

            /// <summary>
            /// True se il contatore è completo
            /// </summary>
            [HideInInspector]
            public bool IsComplete;
            /// <summary>
            /// Contatore
            /// </summary>
            [HideInInspector]
            public int Counter;
        }
    }
}
