using UnityEngine;

namespace Sangaku
{
    public class PlayerControllerSMSetupState : PlayerControllerSMStateBase
    {
        public override void Enter()
        {
            context.PlayerControllerEntity.SetUpEntity();
            Debug.Log("Player entity setupped!");
        }
    } 
}