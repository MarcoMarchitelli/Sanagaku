using UnityEngine;

namespace Sangaku
{
    public class LossListener : MonoBehaviour
    {
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
            gameObject.SetActive(false);
        }
    }
}