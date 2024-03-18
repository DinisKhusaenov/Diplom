using UnityEngine;

public abstract class AirbornState : MovementState
{
    private readonly AirbornStateConfig _config;
    private Character _character;

    protected AirbornState(IStateSwitcher stateSwitcher, Character character, StateMachineData data, Camera camera) : base(stateSwitcher, character, data, camera)
    {
        _config = character.Config.AirbornStateConfig;
        _character = character;
    }

    public override void Enter()
    {
        base.Enter();

        if (_character.JoinHandler.IsJoinedMe)
            Data.Speed = _config.Speed * MultiplyingWhenCombining;
        else
            Data.Speed = _config.Speed;

        View.StartAirborne();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopAirborne();
    }

    public override void Update()
    {
        base.Update();

        Data.YVelocity -= _config.BaseGravity * Time.deltaTime;
    }
}
