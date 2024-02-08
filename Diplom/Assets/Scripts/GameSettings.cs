using Photon.Pun;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Button _quit;

    private PlayerCollector _playerCollector;
    private IJoinHandler _joinHandler;

    private bool _isActive;

    public bool IsActive => _isActive;

    [Inject]
    private void Construct(PlayerCollector playerCollector, IJoinHandler joinHandler)
    {
        _playerCollector = playerCollector;
        _joinHandler = joinHandler;

        Hide();
    }

    private void OnEnable()
    {
        _quit.onClick.AddListener(LeaveRoom);
    }

    private void OnDisable()
    {
        _quit.onClick.RemoveListener(LeaveRoom);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _isActive = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _isActive = false;
    }

    private void LeaveRoom()
    {
        _playerCollector.RemovePlayer();
        _joinHandler.OnQuitGameClicked();

        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene((int)SceneID.LobbyScene);
    }
}
