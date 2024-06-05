using System.Collections.Generic;
using System.Linq;

public class CharacterStateMachine: IStateSwitcher
{
    private List<IState> _states;
    private IState _currentState;

    public CharacterStateMachine(Character character)
    {
        StateMachineData data = new StateMachineData();

        _states = new List<IState>()
        {
            new IdlingState(this, character, data, character.Camera),
            new RunningState(this, character, data, character.Camera),
            new FallingState(this, character, data, character.Camera),
            new JumpingState(this, character, data, character.Camera),
        };

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<State>() where State : IState
    {
        IState state = _states.FirstOrDefault(state => state is State);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void HandleInput() => _currentState.HandleInput();

    public void Update() => _currentState.Update();
}
