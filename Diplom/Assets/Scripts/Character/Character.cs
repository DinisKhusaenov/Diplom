using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [SerializeField] private CharacterConfig _config;
    [SerializeField] private CharacterView _view;
    [SerializeField] private GroundChecker _groundChecker;

    private CharacterInput _input;
    private CharacterStateMachine _stateMachine;
    private CharacterController _characterController;
    private Camera _camera;

    public CharacterInput Input => _input;
    public CharacterController Controller => _characterController;
    public CharacterConfig Config => _config;
    public CharacterView View => _view;
    public GroundChecker GroundChecker => _groundChecker;
    public Camera Camera => _camera;

    private void Awake()
    {
        _view.Initialize();
        _characterController = GetComponent<CharacterController>();
        _input = new CharacterInput();
        _camera = Camera.main;
        _stateMachine = new CharacterStateMachine(this);
    }

    private void Update()
    {
        _stateMachine.HandleInput();

        _stateMachine.Update();
    }

    private void OnEnable() => _input.Enable();

    private void OnDisable() => _input.Disable();
}
