using System.Collections;
using UnityEngine;

namespace Sangaku
{
    /// <summary>
    /// Classe che gestisce le corutine per le classi che non derivano da MonoBehaviour
    /// </summary>
    public class CoroutineManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton
        /// </summary>
        static CoroutineManager singleton;

        /// <summary>
        /// Awake
        /// </summary>
        void Awake()
        {
            // Singleton
            if (singleton == null)
                singleton = this;
            else
                DestroyImmediate(this);
        }

        /// <summary>
        /// Funzione che si occupa di fermare tutte le coroutine
        /// </summary>
        public static void StopAllRoutine()
        {
            if (singleton != null)
                singleton.StopAllCoroutines();
        }

        #region Routine
        /// <summary>
        /// Funzione che lancia una coroutine
        /// </summary>
        /// <param name="_coroutine"></param>
        public static void RunRoutine(IEnumerator _coroutine)
        {
            if (singleton != null)
                singleton.StartCoroutine(_coroutine);
        }

        /// <summary>
        /// Funzione che ferma una coroutine
        /// </summary>
        /// <param name="_coroutine"></param>
        public static void StopRoutine(IEnumerator _coroutine)
        {
            if (singleton != null && _coroutine != null)
                singleton.StopCoroutine(_coroutine);
        }
        #endregion
    }
}

