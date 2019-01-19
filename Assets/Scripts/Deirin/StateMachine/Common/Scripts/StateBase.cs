using UnityEngine;
using UnityEngine.Animations;

namespace Deirin.StateMachine {

    public abstract class StateBase : StateMachineBehaviour, IState {

        #region Events

        protected StateEvent onStateEnd;

        public StateEvent OnStateEnd { get { return onStateEnd; } set { onStateEnd = value; } }

        #endregion

        #region Variables

        [SerializeField] private string id;
        private IContext context;

        #endregion

        #region Properties

        public string ID { get { return id; } }

        public IContext Context { get { return context; } set { context = value; } }

        #endregion

        #region Base behaviour methods

        public virtual void SetUp(IContext _context)
        {
            context = _context;
        }

        public virtual void Enter() {
            
        }

        public virtual void Tick() {

        }

        public virtual void Exit() {
            OnStateEnd.Invoke(this);
        }

        #endregion

        #region StateMachineBehaviour methods

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineEnter(animator, stateMachinePathHash);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) {
            base.OnStateUpdate(animator, stateInfo, layerIndex, controller);
            Tick();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateExit(animator, stateInfo, layerIndex);
            Exit();
        }

        #endregion

    }
}