using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraSetup : MonoBehaviour
{
    private Transform _target;
    private CinemachineVirtualCamera _virtualCamera;

    private IJoinHandler _character;
    private PlayerCollector _players;

    public void Initialize(Transform target, IJoinHandler character, PlayerCollector players)
    {
        _target = target;
        _character = character;
        _players = players;

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _character.YesPressed += TryChangeTarget;
        _character.UnjoinPressed += SetTarget;

        SetTarget();
    }

    private void OnDisable()
    {
        _character.YesPressed -= TryChangeTarget;
        _character.UnjoinPressed -= SetTarget;
    }

    private void SetTarget()
    {
        if (_target != null)
            SetCameraTarget(_target);
    }

    private void TryChangeTarget()
    {
        foreach (var player in _players.Players)
        {
            if (player != null && player != _target)
                SetCameraTarget(player);
        }
    }

    private void SetCameraTarget(Transform target)
    {
        _virtualCamera.Follow = target;
        _virtualCamera.LookAt = target;
    }
}
