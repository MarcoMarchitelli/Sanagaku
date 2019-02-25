using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sangaku
{
    public class OrbAttractionBehaviour : BaseBehaviour
    {
        PlayerController playerCtrl;
        PlayerInputBehaviour playerInput;

        protected override void CustomSetup()
        {
            playerCtrl = FindObjectOfType<PlayerController>();
            playerInput = playerCtrl.GetComponent<PlayerInputBehaviour>();
            playerInput.OnAttractionPressed.AddListener(MoveTowardsPlayer);
        }

        public override void Enable(bool _value)
        {
            base.Enable(_value);
            if (IsSetupped)
                playerInput.OnAttractionPressed.AddListener(MoveTowardsPlayer);
            else
                playerInput.OnAttractionPressed.RemoveListener(MoveTowardsPlayer);
        }

        private void Update()
        {

        }     

        void MoveTowardsPlayer()
        {

        }
    }
}
