using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sangaku
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        public MainSMController mainSM;
        public ObjectPooler objectPooler;

        #region Singleton

        public static GameManager Instance;

        void Singleton()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
        }

        #endregion

        private void Awake()
        {
            Singleton();
            objectPooler.Init();
            mainSM.SetUpSM();
        }
    }
}