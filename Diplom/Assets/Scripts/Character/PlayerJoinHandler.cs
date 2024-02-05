using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinHandler : MonoBehaviour, IJoinHandler
{
    public event Action Jumped;
    public event Action JoinPressed;
    public event Action UnjoinPressed;
    public event Action YesPressed;
    public event Action JoinedMe;
    public event Action UnjoinedMe;

    private CharacterInput _input;

    public bool IsJoined { get; private set; }
    public bool IsJoinedMe { get; private set; }

    public void Initialize(CharacterInput input)
    {
        _input = input;

        IsJoined = false;
        IsJoinedMe = false;
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += Jump;
        PhotonNetwork.NetworkingClient.EventReceived += Join;
        PhotonNetwork.NetworkingClient.EventReceived += JoinMe;
        PhotonNetwork.NetworkingClient.EventReceived += UnjoinMe;
        _input.Movement.Jump.started += OnJumpPressed;
        _input.JoinRequest.Join.started += OnJoinPressed;
        _input.JoinRequest.Unjoin.started += OnUnjoinPressed;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= Jump;
        PhotonNetwork.NetworkingClient.EventReceived -= Join;
        PhotonNetwork.NetworkingClient.EventReceived -= JoinMe;
        PhotonNetwork.NetworkingClient.EventReceived -= UnjoinMe;
        _input.Movement.Jump.started -= OnJumpPressed;
        _input.JoinRequest.Join.started -= OnJoinPressed;
        _input.JoinRequest.Unjoin.started -= OnUnjoinPressed;
    }

    public void OnYesClicked()
    {
        IsJoined = true;
        YesPressed?.Invoke();

        object[] eventData = new object[] { (byte)PlayerJoinEvents.JoinMe };
        PhotonNetwork.RaiseEvent((byte)PlayerJoinEvents.JoinMe, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);

    }

    private void OnJumpPressed(InputAction.CallbackContext obj)
    {
        if (IsJoined)
        {
            object[] eventData = new object[] { (byte)PlayerJoinEvents.Jump };
            PhotonNetwork.RaiseEvent((byte)PlayerJoinEvents.Jump, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
        }
    }

    private void OnJoinPressed(InputAction.CallbackContext obj)
    {
        if (IsJoined == false && IsJoinedMe == false)
        {
            object[] eventData = new object[] { (byte)PlayerJoinEvents.Join };
            PhotonNetwork.RaiseEvent((byte)PlayerJoinEvents.Join, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
        }
    }

    private void OnUnjoinPressed(InputAction.CallbackContext obj)
    {
        if (IsJoined)
        {
            UnjoinPressed?.Invoke();
            IsJoined = false;

            object[] eventData = new object[] { (byte)PlayerJoinEvents.UnjoinMe };
            PhotonNetwork.RaiseEvent((byte)PlayerJoinEvents.UnjoinMe, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
        }
    }

    private void Jump(EventData obj)
    {
        if (obj.Code == (byte)PlayerJoinEvents.Jump)
            Jumped?.Invoke();
    }

    private void Join(EventData data)
    {
        if (data.Code == (byte)PlayerJoinEvents.Join)
            JoinPressed?.Invoke();
    }

    private void JoinMe(EventData data)
    {
        if (data.Code == (byte)PlayerJoinEvents.JoinMe)
        {
            IsJoinedMe = true;
            JoinedMe?.Invoke();
        }
    }

    private void UnjoinMe(EventData data)
    {
        if (data.Code == (byte)PlayerJoinEvents.UnjoinMe)
        {
            UnjoinedMe?.Invoke();
            IsJoinedMe = false;
        }
    }
}
