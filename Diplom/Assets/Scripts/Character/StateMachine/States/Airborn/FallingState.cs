using UnityEngine;

public class FallingState : AirbornState
{
    private readonly GroundChecker _groundChecker;

    public FallingState(IStateSwitcher stateSwitcher, Character character, StateMachineData data, Camera camera) : base(stateSwitcher, character, data, camera)
        => _groundChecker = character.GroundChecker;

    public override void Enter()
    {
        base.Enter();

        View.StartFalling();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopFalling();
    }

    public override void Update()
    {
        base.Update();

        if (_groundChecker.IsTouches)
        {
            Data.YVelocity = 0;

            if (IsHorizontalInputZero())
                StateSwitcher.SwitchState<IdlingState>();
            else
                StateSwitcher.SwitchState<RunningState>();
        }
    }
}
