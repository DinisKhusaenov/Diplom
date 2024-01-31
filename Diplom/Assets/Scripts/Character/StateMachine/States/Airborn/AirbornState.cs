using UnityEngine;

public abstract class AirbornState : MovementState
{
    private readonly AirbornStateConfig _config;

    protected AirbornState(IStateSwitcher stateSwitcher, Character character, StateMachineData data, Camera camera) : base(stateSwitcher, character, data, camera)
        => _config = character.Config.AirbornStateConfig;

    public override void Enter()
    {
        base.Enter();

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
