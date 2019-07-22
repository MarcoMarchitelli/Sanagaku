using UnityEngine;

namespace Sangaku
{
    public class LossListener : MonoBehaviour
    {
        public UnityVoidEvent OnLossMenuActive;

        void Start()
        {
            TemporaryGameManager.Singleton.OnLossMenuActive += HandleOnLossMenuActive;
        }

        private void OnDisable()
        {
            TemporaryGameManager.Singleton.OnLossMenuActive -= HandleOnLossMenuActive;
        }

        private void HandleOnLossMenuActive()
        {
            OnLossMenuActive.Invoke();
        }
    }
}