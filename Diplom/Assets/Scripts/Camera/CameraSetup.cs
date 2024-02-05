using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraSetup : MonoBehaviour
{
    private Transform _target;
    private CinemachineVirtualCamera _virtualCamera;

    private IJoin _character;

    public void Initialize(Transform target, IJoin character)
    {
        _target = target;
        _character = character;

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _character.YesClicked += TryChangeTarget;

        SetTarget();
    }

    private void OnDisable()
    {
        _character.YesClicked -= TryChangeTarget;
    }

    private void SetTarget()
    {
        if (_target != null)
            SetCameraTarget(_target);
    }

    private void TryChangeTarget()
    {
        foreach (var player in PlayersCollector.Players)
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
