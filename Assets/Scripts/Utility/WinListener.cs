using UnityEngine;

namespace Sangaku
{
    public class WinListener : MonoBehaviour
    {
        public UnityVoidEvent OnWinMenuActive;

        void Start()
        {
            TemporaryGameManager.Singleton.OnWinMenuActive += HandleOnWinMenuActive;
        }

        private void OnDisable()
        {
            TemporaryGameManager.Singleton.OnWinMenuActive -= HandleOnWinMenuActive;
        }

        private void HandleOnWinMenuActive()
        {
            OnWinMenuActive.Invoke();
        }
    }
}