namespace Deirin.StateMachine
{  
    public interface IState
    {
        /// <summary>
        /// Event invoked on state exit. Passes data to the state machine.
        /// </summary>
        StateEvent OnStateEnd { get; set; }

        /// <summary>
        /// Identificates the state.
        /// </summary>
        string ID { get; }

        /// <summary>
        /// All the data needed to perform, in class form.
        /// </summary>
        IContext Context { get; set; }

        /// <summary>
        /// Called once on state enter.
        /// </summary>
        void Enter();

        /// <summary>
        /// Called every application lifecycle (Update) if state is active.
        /// </summary>
        void Tick();

        /// <summary>
        /// Called once on state exit.
        /// </summary>
        void Exit();

        /// <summary>
        /// Called for SetUp.
        /// </summary>
        /// <param name="_context">The context data needed to perform.</param>
        void SetUp(IContext _context);
    }
}
