using UnityEngine;
using Deirin.StateMachine;

namespace Sangaku
{
    public class MainSMController : StateMachineBase
    {
        [Header("Context Data")]
        [SerializeField] GameObject mainMenu;

        protected override void ContextSetup()
        {
            context = new MainSMContext(mainMenu, GameManager.Instance);
        }

        protected override void OnStateChange(IState _endedState)
        {

        }

    }

    public class MainSMContext : IContext
    {
        public GameObject MainMenu;
        public GameManager GameManager;

        public MainSMContext(GameObject _mainMenu, GameManager _gameManager)
        {
            MainMenu = _mainMenu;
            GameManager = _gameManager;
        }
    }

}