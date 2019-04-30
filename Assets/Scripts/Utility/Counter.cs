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
        /// True se il counter si resetta da solo dopo il completamente, false altrimenti
        /// </summary>
        [SerializeField] bool autoReset;
        /// <summary>
        /// Lista di confronti da eseguire
        /// </summary>
        [SerializeField] List<CounterGroup> counters = new List<CounterGroup>();

        /// <summary>
        /// Contatore
        /// </summary>
        int counter;

        /// <summary>
        /// Funzione che aggiunge al contatore il numero passato
        /// </summary>
        /// <param name="_number"></param>
        public void Add(int _number)
        {
            counter += _number;
            CheckAll();
        }

        /// <summary>
        /// Funzione che sottrae al contatore il numero passato
        /// </summary>
        /// <param name="_number"></param>
        public void Subtract(int _number)
        {
            counter -= _number;
            CheckAll();
        }

        /// <summary>
        /// Funzione che incrementa di uno il contatore
        /// </summary>
        public void Increase()
        {
            counter++;
            CheckAll();
        }

        /// <summary>
        /// Funzione che decrementa di uno il contatore
        /// </summary>
        public void Decrease()
        {
            counter--;
            CheckAll();
        }

        public void ResetCounter()
        {
            counter = 0;
        }

        /// <summary>
        /// Funzione che si occupa di eseguire il controllo di tutti i counters
        /// </summary>
        void CheckAll()
        {
            for (int i = 0; i < counters.Count; i++)
            {
                if (!counters[i].isComplete)
                    CheckCounter(counters[i]);
            }

            for (int i = 0; i < counters.Count; i++)
            {
                if (!counters[i].isComplete)
                    return;
            }

            if (autoReset)
                ResetCounter();
        }

        /// <summary>
        /// Funzione che si occupa di esegure la comparazione di un counter
        /// </summary>
        void CheckCounter(CounterGroup _counter)
        {
            switch (_counter.comparison)
            {
                case ComparisonType.Equals:
                    if (counter == _counter.comparisonNumber)
                    {
                        _counter.onComparisonSucceded.Invoke();
                        _counter.isComplete = true;
                    }
                    break;
                case ComparisonType.Greater:
                    if (counter > _counter.comparisonNumber)
                    {
                        _counter.onComparisonSucceded.Invoke();
                        _counter.isComplete = true;
                    }
                    break;
                case ComparisonType.Lesser:
                    if (counter < _counter.comparisonNumber)
                    {
                        _counter.onComparisonSucceded.Invoke();
                        _counter.isComplete = true;
                    }
                    break;
                case ComparisonType.GreaterEquals:
                    if (counter >= _counter.comparisonNumber)
                    {
                        _counter.onComparisonSucceded.Invoke();
                        _counter.isComplete = true;
                    }
                    break;
                case ComparisonType.LesserEquals:
                    if (counter <= _counter.comparisonNumber)
                    {
                        _counter.onComparisonSucceded.Invoke();
                        _counter.isComplete = true;
                    }
                    break;
            }
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
            public UnityVoidEvent onComparisonSucceded;
            /// <summary>
            /// Tipo di confronto scelto
            /// </summary>
            public ComparisonType comparison;
            /// <summary>
            /// Numero di confrnto
            /// </summary>
            public int comparisonNumber;
            /// <summary>
            /// True se il contatore è completo
            /// </summary>
            //[HideInInspector]
            public bool isComplete;
        }
    }
}
