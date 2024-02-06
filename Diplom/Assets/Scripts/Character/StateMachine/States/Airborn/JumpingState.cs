using UnityEngine;

public class JumpingState : AirbornState
{
    private readonly JumpingStateConfig _config;

    private Character _character;

    public JumpingState(IStateSwitcher stateSwitcher, Character character, StateMachineData data, Camera camera) : base(stateSwitcher, character, data, camera)
    {
        _config = character.Config.AirbornStateConfig.JumpingStateConfig;
        _character = character;
    }

    public override void Enter()
    {
        base.Enter();

        if (_character.JoinHandler.IsJoinedMe)
            Data.YVelocity = _config.StartYVelocity * MultiplyingWhenCombining;
        else
            Data.YVelocity = _config.StartYVelocity;

        View.StartJumping();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopJumping();
    }

    public override void Update()
    {
        base.Update();

        if (Data.YVelocity < 0)
            StateSwitcher.SwitchState<FallingState>();
    }
}
