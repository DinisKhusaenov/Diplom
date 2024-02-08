using Photon.Pun;
using UnityEngine;

public class Bootstrap : MonoBehaviourPunCallbacks
{
    [SerializeField] private Character _character;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private CameraModeSwitcher _camera;
    [SerializeField] private JoinRequestView _joinRequestView;
    [SerializeField] private JoinActiveView _joinActiveView;
    [SerializeField] private GameSettings _gameSettings;


    private void Awake()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameSettings.IsActive == false)
                _gameSettings.Show();
            else
                _gameSettings.Hide();
        }
    }
}
