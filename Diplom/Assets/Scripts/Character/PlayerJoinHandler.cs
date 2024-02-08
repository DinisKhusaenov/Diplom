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
        PhotonNetwork.NetworkingClient.EventReceived += OnQuitGame;

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
        PhotonNetwork.NetworkingClient.EventReceived -= OnQuitGame;

        _input.Movement.Jump.started -= OnJumpPressed;
        _input.JoinRequest.Join.started -= OnJoinPressed;
        _input.JoinRequest.Unjoin.started -= OnUnjoinPressed;
    }

    public void OnYesClicked()
    {
        IsJoined = true;
        YesPressed?.Invoke();

        object[] eventData = new object[] { (byte)JoinEvents.JoinMe };
        PhotonNetwork.RaiseEvent((byte)JoinEvents.JoinMe, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);

    }

    public void OnQuitGameClicked()
    {
        object[] eventData = new object[] { (byte)JoinEvents.QuitGame };
        PhotonNetwork.RaiseEvent((byte)JoinEvents.QuitGame, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
    }

    private void OnJumpPressed(InputAction.CallbackContext obj)
    {
        if (IsJoined)
        {
            object[] eventData = new object[] { (byte)JoinEvents.Jump };
            PhotonNetwork.RaiseEvent((byte)JoinEvents.Jump, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
        }
    }

    private void OnJoinPressed(InputAction.CallbackContext obj)
    {
        if (IsJoined == false && IsJoinedMe == false)
        {
            object[] eventData = new object[] { (byte)JoinEvents.Join };
            PhotonNetwork.RaiseEvent((byte)JoinEvents.Join, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
        }
    }

    private void OnUnjoinPressed(InputAction.CallbackContext obj)
    {
        if (IsJoined)
        {
            UnjoinPressed?.Invoke();
            IsJoined = false;

            object[] eventData = new object[] { (byte)JoinEvents.UnjoinMe };
            PhotonNetwork.RaiseEvent((byte)JoinEvents.UnjoinMe, eventData, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, SendOptions.SendReliable);
        }
    }

    private void Jump(EventData obj)
    {
        if (obj.Code == (byte)JoinEvents.Jump)
            Jumped?.Invoke();
    }

    private void Join(EventData data)
    {
        if (data.Code == (byte)JoinEvents.Join)
            JoinPressed?.Invoke();
    }

    private void JoinMe(EventData data)
    {
        if (data.Code == (byte)JoinEvents.JoinMe)
        {
            IsJoinedMe = true;
            JoinedMe?.Invoke();
        }
    }

    private void UnjoinMe(EventData data)
    {
        if (data.Code == (byte)JoinEvents.UnjoinMe)
        {
            UnjoinedMe?.Invoke();
            IsJoinedMe = false;
        }
    }

    private void OnQuitGame(EventData data)
    {
        if (IsJoined && data.Code == (byte)JoinEvents.QuitGame)
        {
            UnjoinPressed?.Invoke();
            IsJoined = false;
        }
    }
}
