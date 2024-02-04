using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PhotonView))]
public class Character : MonoBehaviour, IPunObservable
{
    public event Action Jumped;
    public event Action JoinClicked;

    [SerializeField] private CharacterConfig _config;
    [SerializeField] private CharacterView _view;
    [SerializeField] private GroundChecker _groundChecker;

    private CharacterInput _input;
    private CharacterStateMachine _stateMachine;
    private CharacterController _characterController;
    private Camera _camera;
    private PhotonView _photonView;

    public bool IsJoining { get; private set; }

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

        _photonView = GetComponent<PhotonView>();

        IsJoining = false;
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
        PhotonNetwork.NetworkingClient.EventReceived += Jump;
        PhotonNetwork.NetworkingClient.EventReceived += Join;
        _input.Movement.Jump.started += OnJumpPressed;
        _input.JoinRequest.Join.started += OnJoinRequestPressed;
    }

    private void OnDisable()
    {
        _input.Disable();
        PhotonNetwork.NetworkingClient.EventReceived -= Jump;
        PhotonNetwork.NetworkingClient.EventReceived -= Join;
        _input.Movement.Jump.started -= OnJumpPressed;
        _input.JoinRequest.Join.started -= OnJoinRequestPressed;
    }

    private void OnJumpPressed(InputAction.CallbackContext obj)
    {
        if (IsJoining)
        {
            object[] eventData = new object[] { (byte)EventCodes.Jump };
            PhotonNetwork.RaiseEvent((byte)EventCodes.Jump, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
        }
    }

    private void Jump(EventData obj)
    {
        if (obj.Code == (byte)EventCodes.Jump)
            Jumped?.Invoke();
    }

    private void OnJoinRequestPressed(InputAction.CallbackContext obj)
    {
        if (IsJoining == false)
        {
            object[] eventData = new object[] { (byte)EventCodes.Join };
            PhotonNetwork.RaiseEvent((byte)EventCodes.Join, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
        }
    }

    private void Join(EventData data)
    {
        if (data.Code == (byte)EventCodes.Join)
            JoinClicked?.Invoke();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

    public enum EventCodes : byte
    {
        Jump = 1,
        Join
    }
}
