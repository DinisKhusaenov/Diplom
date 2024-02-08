using Photon.Pun;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PhotonView))]
public class Character : MonoBehaviour
{

    [SerializeField] private CharacterConfig _config;
    [SerializeField] private CharacterView _view;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private PlayerJoinHandler _joinHandler; 

    private CharacterInput _input;
    private CharacterStateMachine _stateMachine;
    private CharacterController _characterController;
    private Camera _camera;
    private PhotonView _photonView;

    public CharacterInput Input => _input;
    public CharacterController Controller => _characterController;
    public CharacterConfig Config => _config;
    public CharacterView View => _view;
    public GroundChecker GroundChecker => _groundChecker;
    public Camera Camera => _camera;
    public IJoinHandler JoinHandler => _joinHandler;

    private void Awake()
    {
        _view.Initialize();
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        _input = new CharacterInput();
        _stateMachine = new CharacterStateMachine(this);
        _joinHandler.Initialize(_input);

        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!_photonView.IsMine) return;

        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}