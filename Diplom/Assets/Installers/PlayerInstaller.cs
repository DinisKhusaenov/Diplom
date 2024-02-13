using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstallerPunCallbacks
{
    [SerializeField] private Character _character;
    [SerializeField] private CameraModeSwitcher _camera;
    [SerializeField] private JoinRequestView _joinRequestView;
    [SerializeField] private JoinActiveView _joinActiveView;
    [SerializeField] private List<Transform> _spawnPoints;

    private PlayerCollector _playerCollector;
    private PlayerJoinHandler _playerJoinHandler;

    public override void InstallBindings()
    {
        var character = PhotonNetwork.Instantiate(_character.name, Vector3.zero, Quaternion.identity);

        _playerCollector = character.GetComponent<PlayerCollector>();
        _playerJoinHandler = character.GetComponent<PlayerJoinHandler>();
        
        _camera.Initialize(character.transform, _playerJoinHandler, _playerCollector);

        BindPlayerCollector();
        BindJoinHandler();
        BindMediator();
        BindSpawner(character);
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
        Container.Bind<JoinMediator>().AsSingle().WithArguments(_joinRequestView, _joinActiveView).NonLazy();
    }

    private void BindSpawner(GameObject character)
    {
        Container.Bind<PlayerSpawner>().AsSingle().WithArguments(_spawnPoints, character).NonLazy();
    }
}
