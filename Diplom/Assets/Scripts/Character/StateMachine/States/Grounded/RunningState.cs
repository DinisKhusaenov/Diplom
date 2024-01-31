using UnityEngine;

public class RunningState : GroundedState
{
    private RunningStateConfig _config;

    public RunningState(IStateSwitcher stateSwitcher, Character character, StateMachineData data, Camera camera) : base(stateSwitcher, character, data, camera)
        => _config = character.Config.RunningStateConfig;

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _config.Speed;

        View.StartRunning();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopRunning();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();
    }
}
