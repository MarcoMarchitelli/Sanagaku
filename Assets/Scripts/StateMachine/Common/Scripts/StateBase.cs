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

        #endregion

        #region Properties

        public string ID { get { return id; } }

        public IContext Context { get; set; }

        #endregion

        #region Base behaviour methods

        public abstract void SetUp(IContext _context);

        public virtual void Enter() {
            
        }

        public virtual void Tick() {

        }

        public virtual void Exit() {
            OnStateEnd.Invoke(this);
        }

        #endregion

        #region StateMachineBehaviour methods

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            Enter();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            Tick();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateExit(animator, stateInfo, layerIndex);
            Exit();
        }

        #endregion

    }
}