using Photon.Pun;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstallerPunCallbacks
{
    [SerializeField] private Character _character;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private CameraModeSwitcher _camera;
    [SerializeField] private JoinRequestView _joinRequestView;
    [SerializeField] private JoinActiveView _joinActiveView;

    private PlayerCollector _playerCollector;
    private PlayerJoinHandler _playerJoinHandler;

    public override void InstallBindings()
    {
        var character = PhotonNetwork.Instantiate(_character.name, _spawnPosition.position, Quaternion.identity);

        _playerCollector = character.GetComponent<PlayerCollector>();
        _playerJoinHandler = character.GetComponent<PlayerJoinHandler>();
        
        _camera.Initialize(character.transform, _playerJoinHandler, _playerCollector);

        BindPlayerCollector();
        BindJoinHandler();
        BindMediator();
    }

    private void BindPlayerCollector()
    {
        Container.Bind<PlayerCollector>().FromInstance(_playerCollector).AsSingle();
    }

    private void BindJoinHandler()
    {
        Container.BindInterfacesAndSelfTo<PlayerJoinHandler>().FromInstance(_playerJoinHandler).AsSingle();
    }

    private void BindMediator()
    {
        Container.Bind<JoinMediator>().AsSingle().WithArguments(_joinRequestView, _joinActiveView);
    }
}
