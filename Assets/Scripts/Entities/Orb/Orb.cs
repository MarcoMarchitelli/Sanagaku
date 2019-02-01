using UnityEngine;

namespace Sangaku
{
    public class Orb : BaseEntity
    {
        public enum State { free, caught}

        private State currentState;
        Transform catchTransform;

        public override void CustomSetup()
        {
            ChangeState(State.free);
        }

        public void ChangeState(State _newState, Transform _catchPoint = null)
        {
            switch (_newState)
            {
                case State.free:
                    foreach (IBehaviour behaviour in Behaviours)
                    {
                        BaseBehaviour b = behaviour as BaseBehaviour;
                        b.enabled = true;
                    }
                    break;
                case State.caught:
                    foreach (IBehaviour behaviour in Behaviours)
                    {
                        BaseBehaviour b = behaviour as BaseBehaviour;
                        b.enabled = false;
                    }
                    if (_catchPoint)
                    {
                        catchTransform = _catchPoint;
                    }
                    break;
            }
        }

        private void Update()
        {
            if(currentState == State.caught)
            {
                transform.position = catchTransform.position;
                transform.rotation = catchTransform.rotation;
            }
        }
    }
}
