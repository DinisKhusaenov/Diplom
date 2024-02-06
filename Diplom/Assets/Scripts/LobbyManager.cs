using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks, IDisposable
{
    private const int MaxPlayers = 2;

    [SerializeField] private Button _create;
    [SerializeField] private Button _join;
    [SerializeField] private TMP_InputField _createInput;
    [SerializeField] private TMP_InputField _joinInput;
    [SerializeField] private GameObject _connecting;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();

        _create.onClick.AddListener(CreateRoom);
        _join.onClick.AddListener(JoinRoom);
        _connecting.SetActive(true);
    }

    public void Dispose()
    {
        _create.onClick.RemoveListener(CreateRoom);
        _join.onClick.RemoveListener(JoinRoom);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        _connecting.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        GoToGameScene();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created successfully!");
        GoToGameScene();
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayers;

        if (!PhotonNetwork.CreateRoom(_createInput.text, roomOptions, TypedLobby.Default))
        {
            Debug.LogError("Failed to create room");
        }
    }

    private void JoinRoom()
    {
        if (PhotonNetwork.JoinRoom(_joinInput.text))
        {
            Debug.Log("Joining room...");
        }
        else
        {
            Debug.LogError("Failed to join room");
        }
    }

    private void GoToGameScene()
    {
        PhotonNetwork.LoadLevel((int)SceneID.GameScene);
    }
}
