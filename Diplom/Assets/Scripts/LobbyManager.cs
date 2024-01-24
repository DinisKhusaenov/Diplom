using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private const int MaxPlayers = 2;

    [SerializeField] private Button _create;
    [SerializeField] private Button _join;
    [SerializeField] private TMP_InputField _createInput;
    [SerializeField] private TMP_InputField _joinInput;

    private void OnEnable()
    {
        _create.onClick.AddListener(CreateRoom);
        _join.onClick.AddListener(JoinRoom);
    }

    private void OnDisable()
    {
        _create.onClick.RemoveListener(CreateRoom);
        _join.onClick.RemoveListener(JoinRoom);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel((int)SceneID.GameScene);
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayers;
        PhotonNetwork.CreateRoom(_createInput.text, roomOptions);
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_joinInput.text);
    }
}
