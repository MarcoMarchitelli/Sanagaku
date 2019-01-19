using UnityEngine;
using Deirin.StateMachine;

public class ExampleStateMachine : StateMachineBase
{

    #region ExampleStateMachine methods

    /// <summary>
    /// Reads the ended state and handles the data from his context, then chooses the next state.
    /// </summary>
    /// <param name="_endedState">Ended state.</param>
    protected override void OnStateChange(IState _endedState)
    {
        switch (_endedState.ID)
        {
            case "Start_State":
                HandleStartState(_endedState.Context);
                break;
            case "Run_State":
                HandleRunState(_endedState.Context);
                break;
            case "Exit_State":
                HandleExitState(_endedState.Context);
                break;
            default:
                break;
        }
    }

    void HandleStartState(IContext _context)
    {
        ResetAllTriggers();
        stateMachineAnimator.SetTrigger("GoToRun");
    }

    void HandleRunState(IContext _context)
    {
        ResetAllTriggers();
        stateMachineAnimator.SetTrigger("GoToExit");
    }

    void HandleExitState(IContext _context)
    {
        ResetAllTriggers();
        stateMachineAnimator.SetTrigger("CloseApplication");
    }

    void ResetAllTriggers()
    {
        foreach (AnimatorControllerParameter parameter in stateMachineAnimator.parameters)
        {
            stateMachineAnimator.ResetTrigger(parameter.name);
        }
    }

    #endregion

    #region MonoBehaviour methods

    private void Start()
    {
        SetUp();
    }

    #endregion

}
