using UnityEngine;
using Deirin.StateMachine;

public class Run_State : StateBase
{
    public override void Enter()
    {
        Debug.Log("Entered state: " + ID);
    }

    public override void Tick()
    {
        Debug.Log("Tick state: " + ID);
        if (Input.GetKeyDown(KeyCode.Space))
            Exit();
    }

    public override void Exit()
    {
        Debug.Log("Exit state: " + ID);
        base.Exit();
    }
}
