using UnityEngine;

public class IdlingState : GroundedState
{
    public IdlingState(IStateSwitcher stateSwitcher, Character character, StateMachineData data, Camera camera) : base(stateSwitcher, character, data, camera)
    {
    }

    public override void Enter()
    {
        base.Enter();

        View.StartIdling();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopIdling();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            return;

        StateSwitcher.SwitchState<RunningState>();
    }
}
