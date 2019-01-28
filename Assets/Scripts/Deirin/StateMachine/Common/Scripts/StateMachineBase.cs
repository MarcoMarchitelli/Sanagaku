using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Deirin.StateMachine {

    [RequireComponent(typeof(Animator))]
    public abstract class StateMachineBase : MonoBehaviour {

        #region Variables

        /// <summary>
        /// The animator attached to this state machine.
        /// </summary>
        protected Animator _stateMachine;
        public Animator StateMachine
        {
            get
            {
                if (!_stateMachine)
                    _stateMachine = GetComponent<Animator>();
                return _stateMachine;
            }
        }
        protected List<IState> states;
        protected IContext context;

        #endregion

        #region Properties

        /// <summary>
        /// List of all states.
        /// </summary>
        public List<IState> States { get { return states; } set { states = value; } }

        #endregion

        #region StateMachine methods

        /// <summary>
        /// Handles the logic behind the state change.
        /// </summary>
        /// <param name="_endedState">The ended state.</param>
        protected abstract void OnStateChange(IState _endedState);

        /// <summary>
        /// Fills States list. Subscribes to every state's event.
        /// </summary>
        public virtual void SetUpSM()
        {
            //gets all the states from the animator component as IState.
            States = StateMachine.GetBehaviours<StateBase>().ToList<IState>();

            //subscribes to every state's end event
            foreach (IState state in States)
            {
                state.SetUp(context);
                state.OnStateEnd += OnStateChange;
            }
        }

        #endregion

    }
}