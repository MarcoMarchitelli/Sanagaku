using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    public class MenuTrigger : MonoBehaviour
    {
        public void ShowLossMenu()
        {
            TemporaryGameManager.Singleton.GoToLossMenu();
        }

        public void ShowWinMenu()
        {
            TemporaryGameManager.Singleton.GoToWinMenu();
        }

        public void ShowMainMenu()
        {
            TemporaryGameManager.Singleton.GoToMainMenu();
        }

        public void ShowGameplay()
        {
            TemporaryGameManager.Singleton.GoToGameplay();
        }
        public void ShowPauseMenu()
        {
            TemporaryGameManager.Singleton.GoToPauseMenu();
        }

        public void ShowLoadingPanel()
        {
            TemporaryGameManager.Singleton.GoToLoadingPanel();
        }
    }
}